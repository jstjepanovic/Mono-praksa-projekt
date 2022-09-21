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
    public class UserRepository : IUserRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");
        private async Task<bool> UserIdExistsAsync(Guid userId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    SqlCommand commandCheckId = new SqlCommand(
                    "SELECT Count (*) FROM TableUser WHERE UserId=@userId", connection);
                    commandCheckId.Parameters.AddWithValue("@userId", userId);
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

        public async Task<List<User>> FindUserAsync(Paging paging, Sorting sorting, UserFilter userFilter)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    List<User> users = new List<User>();

                    StringBuilder stringBuilder = new StringBuilder("SELECT * FROM TableUser ", 500);

                    if (userFilter.Search != null)
                    {
                        stringBuilder.AppendFormat("WHERE Username LIKE '{0}' ", userFilter.Search);
                    }
                   
                    stringBuilder.AppendFormat("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder);
                    
                    stringBuilder.Append("OFFSET @page ROWS FETCH NEXT @rpp ROWS ONLY;");
                    
    
                    SqlCommand commandGet = new SqlCommand(Convert.ToString(stringBuilder), connection);
                    commandGet.Parameters.AddWithValue("@page", (paging.PageNumber - 1) * paging.RecordsByPage);
                    commandGet.Parameters.AddWithValue("@rpp", paging.RecordsByPage);

                    SqlDataReader reader = await commandGet.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetDateTime(5)));
                        }
                        reader.Close();
                        return users; 
                    }
                    else
                    {
                        throw new Exception("User doesn't exist.");

                    }


                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            if (await UserIdExistsAsync(userId) == false)
            {
                throw new Exception("No user with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM TableUser WHERE UserId=@userId", connection);
                command.Parameters.AddWithValue("@userId", userId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        User user = new User(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetDateTime(5));
                        return user;
                    }
                    else
                    {
                        throw new Exception("User does not exist!");
                    }

                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (await UserIdExistsAsync(user.UserId) == true)
            {
                throw new Exception("User already exists!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("INSERT INTO TableUser(UserId, UserName, Email, Password, TimeCreated, TimeUpdated) VALUES(@userId, @name, @email, @pass, @timeCreated, @timeUpdated)", connection);
                command.Parameters.AddWithValue("@userId", user.UserId);
                command.Parameters.AddWithValue("@name", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@pass", user.Password);
                command.Parameters.AddWithValue("@timeCreated", user.TimeCreated);
                command.Parameters.AddWithValue("@timeUpdated", user.TimeUpdated);
                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
            return user;
        }

        public async Task<User> UpdateUserAsync(Guid userId, User user)
        {
            if (await UserIdExistsAsync(userId) == false)
            {
                throw new Exception("No user with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("UPDATE TableUser SET UserName = @userName, Email = @email, Password = @pass TimeUpdated = @time where UserId = @userId", connection);           
                command.Parameters.AddWithValue("@userName", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@pass", user.Password);
                command.Parameters.AddWithValue("@time", user.TimeUpdated);
                command.Parameters.AddWithValue("@userId", userId);
                user.UserId = userId;
                try
                {
                    await command.ExecuteNonQueryAsync();
                    return await GetUserAsync(userId);
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            if (await UserIdExistsAsync(userId) == false)
            {
                throw new Exception("No user with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "DELETE FROM TableUser WHERE UserId = @userId;", connection);
                command.Parameters.AddWithValue("@userId", userId);
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


