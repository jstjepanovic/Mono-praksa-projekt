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
    public class CategoryRepository : ICategoryRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                List<Category> categorys = new List<Category>();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Category", connection);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            categorys.Add(new Category(reader.GetGuid(0), reader.GetString(1), reader.GetString(2)));
                        }
                    }
                    return categorys;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<Category> GetCategoryAsync(Guid categoryId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Category WHERE CategoryId = @categoryId", connection);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Category category = new Category(reader.GetGuid(0), reader.GetString(1), reader.GetString(2));
                        return category;
                    }
                    else
                    {
                        throw new Exception("Genre does not exists!");
                    }
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }
    }
}
