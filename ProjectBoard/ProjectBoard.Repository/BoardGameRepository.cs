using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");

        public async Task<int> BoardGameCheckAsync(Guid boardGameId)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("select count(*) from BoardGame where BoardGameId = @ID", con))
                {
                    int boardGameCount;
                    count.Parameters.AddWithValue("@ID", boardGameId);
                    try
                    {
                        await con.OpenAsync();                      
                        boardGameCount = (int)await count.ExecuteScalarAsync();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                    return boardGameCount;
                }
            }
        }

        public async Task<List<BoardGame>> FindBoardGameAsync(Paging paging, Sorting sorting, BoardGameFilter filter)
        {
            SqlConnection con = new SqlConnection(conStr);
            List<BoardGame> boardGames = new List<BoardGame>();
            using (con)
            {

                SqlCommand command = new SqlCommand
                {
                    Connection = con
                };

                StringBuilder stringBuilder = new StringBuilder("select * from BoardGame bg join Genre g on (bg.GenreId = g.GenreId) where 1=1 ");

                if (filter.Name != null)
                {
                    stringBuilder.Append("and bg.Name like @Name ");
                    command.Parameters.AddWithValue("@Name", "%" + filter.Name + "%");
                }

                if (filter.MinPlayers != null)
                {
                    stringBuilder.Append("and NoPlayersMin >= @MinPlayers ");
                    command.Parameters.AddWithValue("@MinPlayers", filter.MinPlayers);
                }

                if (filter.MaxPlayers != null)
                {
                    stringBuilder.Append("and NoPlayersMax <= @MaxPlayers ");
                    command.Parameters.AddWithValue("@MaxPlayers", filter.MaxPlayers);
                }

                if (filter.Age != null)
                {
                    stringBuilder.Append("and Age >= @Age ");
                    command.Parameters.AddWithValue("@Age", filter.Age);
                }

                if (filter.Rating != null)
                {
                    stringBuilder.Append("and Rating >= @Rating ");
                    command.Parameters.AddWithValue("@Rating", filter.Rating);
                }

                if (filter.Weight != null)
                {
                    stringBuilder.Append("and Weight >= @Weight ");
                    command.Parameters.AddWithValue("@Weight", filter.Weight);
                }

                if (filter.Publisher != null)
                {
                    stringBuilder.Append("and Publisher like @Publisher ");
                    command.Parameters.AddWithValue("@Publisher", "%" + filter.Publisher + "%");
                }

                if (filter.TimeUpdated != null)
                {
                    stringBuilder.Append("and TimeUpdated >= @TimeUpdated ");
                    command.Parameters.AddWithValue("@TimeUpdated", filter.TimeUpdated);
                }

                if (sorting.OrderBy == "Rating" || sorting.OrderBy == "Weight")
                {
                    stringBuilder.Append(string.Format("order by isnull({0}, 0) {1} ", sorting.OrderBy, sorting.SortOrder));
                }else
                {
                    stringBuilder.Append(string.Format("order by bg.{0} {1} ", sorting.OrderBy, sorting.SortOrder));
                }


                if (!(paging.PageNumber == null || paging.RecordsByPage == null))
                {
                    stringBuilder.Append("offset @offset rows fetch next @rpp rows only;");
                    int? offset = (paging.PageNumber - 1) * paging.RecordsByPage;
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters.AddWithValue("@rpp", paging.RecordsByPage);
                }

                command.CommandText = stringBuilder.ToString();

                await con.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                        boardGames.Add(new BoardGame(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                                                     reader.IsDBNull(6) ? 0 : reader.GetDouble(6), reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                                                     reader.GetString(8), reader.GetGuid(9), reader.GetDateTime(10), reader.GetDateTime(11), new Genre(reader.GetGuid(12), reader.GetString(13))));
                }

                reader.Close();
                return boardGames;
            }
            
        }

        public async Task<BoardGame> GetBoardGameAsync(Guid boardGameId)
        {

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("select * from BoardGame join Genre on (BoardGame.GenreId = Genre.GenreId) where BoardGameId = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", boardGameId);

                    try
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            return new BoardGame(reader.GetGuid(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5),
                                                     reader.IsDBNull(6) ? 0 : reader.GetDouble(6), reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                                                     reader.GetString(8), reader.GetGuid(9), reader.GetDateTime(10), reader.GetDateTime(11), new Genre(reader.GetGuid(12), reader.GetString(13)));
                        }
                        else
                        {
                            throw new Exception("No elements with such Id.");
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                }
            }
        }

        public async Task<int> BoardGameCountAsync()
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("select count(*) from BoardGame", con))
                {
                    int boardGameCount;
                    try
                    {
                        await con.OpenAsync();
                        boardGameCount = (int)await count.ExecuteScalarAsync();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                    return boardGameCount;
                }
            }
        }

        public async Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT INTO BoardGame(BoardGameId, Name, NoPlayersMin, NoPlayersMax, Age, AvgPlayingTime, Publisher, GenreId, TimeCreated, TimeUpdated) 
                                        VALUES(@BoardGameId,@Name,@NoPlayersMin,@NoPlayersMax,@Age,@AvgPlayingTime,@Publisher,@GenreId,@TimeCreated,@TimeUpdated)";

                    cmd.Parameters.AddWithValue("@BoardGameId", boardGame.BoardGameId);
                    cmd.Parameters.AddWithValue("@Name", boardGame.Name);
                    cmd.Parameters.AddWithValue("@NoPlayersMin", boardGame.NoPlayersMin);
                    cmd.Parameters.AddWithValue("@NoPlayersMax", boardGame.NoPlayersMax);
                    cmd.Parameters.AddWithValue("@Age", boardGame.Age);
                    cmd.Parameters.AddWithValue("@AvgPlayingTime", boardGame.AvgPlayingTime);
                    cmd.Parameters.AddWithValue("@Publisher", boardGame.Publisher);
                    cmd.Parameters.AddWithValue("@GenreId", boardGame.GenreId);
                    cmd.Parameters.AddWithValue("@TimeCreated", boardGame.TimeCreated);
                    cmd.Parameters.AddWithValue("@TimeUpdated", boardGame.TimeUpdated);


                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return await GetBoardGameAsync(boardGame.BoardGameId);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }

                }
            }
        }

        public async Task<BoardGame> UpdateBoardGameAsync(Guid boardGameId, BoardGame boardGame)
        {
            int boardGameCheck = await BoardGameCheckAsync(boardGameId);
            if (boardGameCheck == 0)
            {
                throw new Exception("No elements with such ID.");
            }

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(@"update BoardGame set Name = @Name, NoPlayersMin = @NoPlayersMin, NoPlayersMax = @NoPlayersMax, Age = @Age,
                                                        AvgPlayingTime = @AvgTime, Publisher = @Publisher, GenreId = @GenreId,
                                                        TimeUpdated = @TimeUpdated where BoardGameId = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", boardGameId);
                    cmd.Parameters.AddWithValue("@Name", boardGame.Name);
                    cmd.Parameters.AddWithValue("@NoPlayersMin", boardGame.NoPlayersMin);
                    cmd.Parameters.AddWithValue("@NoPlayersMax", boardGame.NoPlayersMax);
                    cmd.Parameters.AddWithValue("@Age", boardGame.Age);
                    cmd.Parameters.AddWithValue("@AvgTime", boardGame.AvgPlayingTime);
                    cmd.Parameters.AddWithValue("@Publisher", boardGame.Publisher);
                    cmd.Parameters.AddWithValue("@GenreId", boardGame.GenreId);
                    cmd.Parameters.AddWithValue("@TimeUpdated", boardGame.TimeUpdated);

                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return boardGame;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }

                }
            }

        }

        public async Task DeleteBoardGameAsync(Guid boardGameId)
        {
            int boardGameCheck = await BoardGameCheckAsync(boardGameId);
            if (boardGameCheck == 0)
            {
                throw new Exception("No elements with such ID.");
            }

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand("delete from BoardGame where BoardGameId = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", boardGameId);

                    try
                    {
                        await con.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                }
            }
        }

    }
}
