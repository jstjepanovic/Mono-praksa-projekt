using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");
        private async Task<bool> OrderIdExistsAsync(Guid orderId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    SqlCommand commandCheckId = new SqlCommand(
                    "SELECT Count (*) FROM TableOrder WHERE OrderId=@orderId", connection);
                    commandCheckId.Parameters.AddWithValue("@orderId", orderId);
                    if ((int)await commandCheckId.ExecuteScalarAsync() > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }
        public async Task<List<Order>> FindOrderAsync(Guid? userId, Paging paging, Sorting sorting, UserFilter userFilter)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    List<Order> orders = new List<Order>();
                    SqlCommand command = new SqlCommand("", connection);
                    StringBuilder stringBuilder = new StringBuilder(@"select o.OrderId, o.DeliveryAddress, o.TimeCreated, o.TimeUpdated, o.UserId, bg.Name
                                                                    from TableOrder o join listing l on (o.OrderId = l.OrderId) join BoardGame bg on (l.BoardGameId = bg.BoardGameId) where 1 = 1 ");
                    if (userId != null)
                    {
                        stringBuilder.Append("and o.UserId = @userId ");
                        command.Parameters.AddWithValue("@userId", userId);
                    }
                    if (userFilter.Search != null)
                    {
                        stringBuilder.AppendFormat("and DeliveryAddress LIKE '{0}%' ", userFilter.Search);
                    }
                    stringBuilder.AppendFormat("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder);

                    stringBuilder.Append("OFFSET @page ROWS FETCH NEXT @rpp ROWS ONLY;");



                    command.Parameters.AddWithValue("@page", (paging.PageNumber - 1) * paging.RecordsByPage);
                    command.Parameters.AddWithValue("@rpp", paging.RecordsByPage);
                    
                    command.CommandText = stringBuilder.ToString();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Order(reader.GetGuid(0), reader.GetString(1),reader.GetDateTime(2), reader.GetDateTime(3), reader.GetGuid(4), reader.GetString(5)));
                        }
                        return orders;
                    }
                    else
                    {
                        throw new Exception("Order doesn't exist.");
                    }

                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No order with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand command = new SqlCommand(@"select o.OrderId, o.DeliveryAddress, o.TimeCreated, o.TimeUpdated, o.UserId, bg.Name
                                                      from TableOrder o join listing l on (o.OrderId = l.OrderId) join BoardGame bg on (l.BoardGameId = bg.BoardGameId) OrderId =@orderId", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Order order = new Order(reader.GetGuid(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetGuid(4), reader.GetString(5));
                        return order;
                    }
                    else
                    {
                        throw new Exception("Order does not exist!");
                    }

                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<Order> CreateOrderAsync(Guid listingId, Order order)
        {
            if (await OrderIdExistsAsync(order.OrderId) == true)
            {
                throw new Exception("Order already exists!");
            }

            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand("INSERT INTO TableOrder(OrderId,DeliveryAddress,TimeCreated,TimeUpdated,UserId) VALUES(@orderId, @deliveryAddress, @timeCreated, @timeUpdated, @userId)", connection);
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@deliveryAddress", order.DeliveryAddress);
                command.Parameters.AddWithValue("@timeCreated", order.TimeCreated);
                command.Parameters.AddWithValue("@timeUpdated", order.TimeUpdated);
                command.Parameters.AddWithValue("@userId", order.UserId);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "update Listing set OrderId = @NewOrderId where ListingId = @listingId";
                    command.Parameters.AddWithValue("@NewOrderId", order.OrderId);
                    command.Parameters.AddWithValue("@listingId", listingId);
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }

                transaction.Commit();
                connection.Close();
                return await GetOrderAsync(order.OrderId);
            }
        }

        public async Task<Order> UpdateOrderAsync(Guid orderId, Order order)
        {
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No order with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                order.OrderId = orderId;
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "UPDATE TableOrder SET DeliveryAddress = @address, TimeUpdated = @timeUpdated WHERE OrderId=@oId", connection);
                command.Parameters.AddWithValue("@address", order.DeliveryAddress);
                command.Parameters.AddWithValue("@timeUpdated", order.TimeUpdated);
                command.Parameters.AddWithValue("@oId", orderId);

                try
                {
                    await command.ExecuteNonQueryAsync();
                    return await GetOrderAsync(orderId);
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            if (await OrderIdExistsAsync(orderId) == false)
            {
                throw new Exception("No order with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "DELETE FROM TableOrder WHERE OrderId = @orderId;", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }
    }
}
