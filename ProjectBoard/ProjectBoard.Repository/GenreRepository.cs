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
    class GenreRepository : IGenreRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");
        public async Task<List<Genre>> GetAllGenreAsync()
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                List<Genre> genres = new List<Genre>();
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Genre", connection);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            genres.Add(new Genre(reader.GetGuid(0), reader.GetString(1)));
                        }
                    }
                    return genres;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }
        public async Task<Genre> GetGenreAsync(Guid genreId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand command = new SqlCommand(
                "SELECT * FROM Genre WHERE GenreId = @genreId", connection);
                command.Parameters.AddWithValue("@genreId", genreId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Genre genre = new Genre(reader.GetGuid(0), reader.GetString(1));
                        return genre;
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
