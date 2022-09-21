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
    public class ListingRepository : IListingRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");
        private async Task<bool> ListingIdExistsAsync(Guid listingId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    SqlCommand commandCheckId = new SqlCommand(
                    "SELECT Count (*) FROM Listing WHERE ListingId=@listingId", connection);
                    commandCheckId.Parameters.AddWithValue("@listingId", listingId);
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

        public async Task<List<Listing>> FindListingAsync(Guid? userId, Paging paging, Sorting sorting, ListingFilter listingFilter)
        {

            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("", connection);
                List<Listing> listings = new List<Listing>();
                StringBuilder stringBuilder = new StringBuilder(@"select l.listingId, l.Price, l.Condition, l.TimeCreated, l.TimeUpdated, l.UserId, l.BoardGameId, l.OrderId, u.Username, bg.Name
                                                                from Listing l join TableUser u on (l.UserId = u.UserId) join BoardGame bg on (l.BoardGameId = bg.BoardGameId)
                                                                where 1 = 1 ");
                if (userId != null)
                {
                    stringBuilder.Append("and l.UserId = @userId ");
                    command.Parameters.AddWithValue("@userId", userId);
                }
                if (listingFilter.Price != null)
                {
                    stringBuilder.Append("and Price>=@price ");
                    command.Parameters.AddWithValue("@price", listingFilter.Price);
                }
                if (listingFilter.Condition != null)
                {
                    stringBuilder.Append("and Condition=@condition ");
                    command.Parameters.AddWithValue("@condition", listingFilter.Condition);
                }
                if (listingFilter.TimeCreated != null)
                {
                    stringBuilder.Append("and TimeCreated>=@timeCreated ");
                    command.Parameters.AddWithValue("@timeCreated", listingFilter.TimeCreated);
                }
                stringBuilder.Append(string.Format("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder));

                if (!(paging.PageNumber == null || paging.RecordsByPage == null))
                {
                    stringBuilder.Append("offset @offset rows fetch next @recordsByPage rows only;");
                    int? offset = (paging.PageNumber - 1) * paging.RecordsByPage;
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@recordsByPage", paging.RecordsByPage);
                }
                

                command.CommandText = stringBuilder.ToString();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        listings.Add(new Listing(reader.GetGuid(0), reader.GetDouble(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4), reader.GetGuid(5), reader.GetGuid(6), reader.IsDBNull(7) ? (Guid?)null : reader.GetGuid(7), reader.GetString(8), reader.GetString(9)));
                    }
                }
                else
                {
                    throw new Exception("Order doesn't exist.");
                }
                return listings;
            }
        }

        public async Task<Listing> GetListingAsync(Guid listingId)
        {
            if (await ListingIdExistsAsync(listingId) == false)
            {
                throw new Exception("No listing with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand command = new SqlCommand(@"select l.listingId, l.Price, l.Condition, l.TimeCreated, l.TimeUpdated, l.UserId, l.BoardGameId, l.OrderId, u.Username, bg.Name
                                                      from Listing l join TableUser u on (l.UserId = u.UserId) join BoardGame bg on (l.BoardGameId = bg.BoardGameId) WHERE ListingId = @listingId", connection);
                command.Parameters.AddWithValue("@listingId", listingId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Listing listing = new Listing(reader.GetGuid(0), reader.GetDouble(1), reader.GetString(2),
                            reader.GetDateTime(3), reader.GetDateTime(4), reader.GetGuid(5), reader.GetGuid(6), reader.IsDBNull(7) ? (Guid?)null : reader.GetGuid(7), reader.GetString(8), reader.GetString(9));
                        return listing;
                    }
                    else
                    {
                        throw new Exception("Listing does not exists!");
                    }
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<Listing> CreateListingAsync(Listing listing)
        {
            if (await ListingIdExistsAsync(listing.ListingId) == true)
            {
                throw new Exception("Listing already exists!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("INSERT INTO Listing (ListingId, Price, Condition, TimeCreated, TimeUpdated, UserId, BoardGameId) VALUES (@listingId, @price, @condition, @timeCreated, @timeUpdated, @userId, @boardGameId)", connection);
                command.Parameters.AddWithValue("@listingId", listing.ListingId);
                command.Parameters.AddWithValue("@price", listing.Price);
                command.Parameters.AddWithValue("@condition", listing.Condition);
                command.Parameters.AddWithValue("@timeCreated", listing.TimeCreated);
                command.Parameters.AddWithValue("@timeUpdated", listing.TimeUpdated);
                command.Parameters.AddWithValue("@userId", listing.UserId);
                command.Parameters.AddWithValue("@boardGameId", listing.BoardGameId);
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
            return await GetListingAsync(listing.ListingId);
        }

        public async Task<Listing> UpdateListingAsync(Guid listingId, Listing listing)
        {
            if (await ListingIdExistsAsync(listingId) == false)
            {
                throw new Exception("No listing with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "UPDATE Listing SET Price = @price, Condition = @condition, TimeUpdated = @timeUpdated WHERE ListingID = @listingId", connection);
                command.Parameters.AddWithValue("@listingId", listingId);
                command.Parameters.AddWithValue("@price", listing.Price);
                command.Parameters.AddWithValue("@condition", listing.Condition);
                command.Parameters.AddWithValue("@timeUpdated", listing.TimeUpdated);
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return await GetListingAsync(listingId);
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<bool> DeleteListingAsync(Guid listingId)
        {
            if (await ListingIdExistsAsync(listingId) == false)
            {
                throw new Exception("No listing with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "DELETE FROM Listing WHERE ListingId = @listingId", connection);
                command.Parameters.AddWithValue("@listingId", listingId);
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
