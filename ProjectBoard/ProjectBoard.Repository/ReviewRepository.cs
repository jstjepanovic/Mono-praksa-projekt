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
    public class ReviewRepository : IReviewRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");

        public async Task<int> ReviewCheckAsync(Guid reviewId)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("select count(*) from Review where ReviewId = @ID", con))
                {
                    int reviewCount;
                    count.Parameters.AddWithValue("@ID", reviewId);
                    try
                    {
                        await con.OpenAsync();
                        reviewCount = (int)await count.ExecuteScalarAsync();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                    return reviewCount;
                }
            }
        }

        public async Task<List<Review>> FindReviewAsync(Guid? boardGameId, Paging paging, Sorting sorting, ReviewFilter filter)
        {
            SqlConnection con = new SqlConnection(conStr);
            List<Review> reviews = new List<Review>();
            using (con)
            {
                int? offset = (paging.PageNumber - 1) * paging.RecordsByPage;

                SqlCommand command = new SqlCommand
                {
                    Connection = con
                };

                StringBuilder stringBuilder = new StringBuilder(@"select ReviewId, r.UserId, r.BoardGameId, r.Rating, r.Weight, ReviewText, r.TimeCreated, r.TimeUpdated, Username, b.Name 
                                                                from Review r join TableUser tu on (r.UserId = tu.UserId) join BoardGame b on (r.BoardGameId = b.BoardGameId) where 1=1 ");

                if (boardGameId != null)
                {
                    stringBuilder.Append("and r.BoardGameId = @BoardGameId ");
                    command.Parameters.AddWithValue("@BoardGameId", boardGameId);
                }

                if (filter.Rating != null)
                {
                    stringBuilder.Append("and r.Rating >= @Rating ");
                    command.Parameters.AddWithValue("@Rating", filter.Rating);
                }

                if (filter.Weight != null)
                {
                    stringBuilder.Append("and r.Weight >= @Weight ");
                    command.Parameters.AddWithValue("@Weight", filter.Weight);
                }

                if (filter.TimeUpdated != null)
                {
                    stringBuilder.Append("and TimeUpdated >= @TimeUpdated ");
                    command.Parameters.AddWithValue("@TimeUpdated", filter.TimeUpdated);
                }

                stringBuilder.Append(string.Format("order by {0} {1} ", sorting.OrderBy, sorting.SortOrder));
                stringBuilder.Append("offset @offset rows fetch next @rpp rows only;");

                command.Parameters.AddWithValue("@offset", offset);
                command.Parameters.AddWithValue("@rpp", paging.RecordsByPage);

                command.CommandText = stringBuilder.ToString();

                await con.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                        reviews.Add(new Review(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetDouble(3), reader.GetDouble(4), reader.IsDBNull(5) ? "" : reader.GetString(5)
                                              , reader.GetDateTime(6), reader.GetDateTime(7), reader.GetString(8), reader.GetString(9)));
                }

                reader.Close();
                return reviews;
            }

        }

        public async Task<Review> GetReviewAsync(Guid reviewId)
        {
            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(@"select ReviewId, r.UserId, r.BoardGameId, r.Rating, r.Weight, ReviewText, r.TimeCreated, r.TimeUpdated, Username, b.Name 
                                                         from Review r join TableUser tu on (r.UserId = tu.UserId) join BoardGame b on (r.BoardGameId = b.BoardGameId)
                                                         where ReviewId = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", reviewId);

                    try
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            return new Review(reader.GetGuid(0), reader.GetGuid(1), reader.GetGuid(2), reader.GetDouble(3), reader.GetDouble(4), reader.IsDBNull(5) ? "" : reader.GetString(5)
                                              , reader.GetDateTime(6), reader.GetDateTime(7), reader.GetString(8), reader.GetString(9));
                        }
                        else
                        {
                            throw new Exception("No data with such Id.");
                        }
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                }
            }
        }

        public async Task<int> CountReviewAsync(Guid boardGameId)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
                using (SqlCommand count = new SqlCommand("select count(*) from Review where BoardGameId=@ID", con))
                {
                    int reviewCount;
                    count.Parameters.AddWithValue("@ID", boardGameId);
                    try
                    {
                        await con.OpenAsync();
                        reviewCount = (int)await count.ExecuteScalarAsync();
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }
                    return reviewCount;
                }
            }
        }
        public async Task<Review> CreateReviewAsync(Review review)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (con)
            {
               
                using (SqlCommand cmd = new SqlCommand())
                {
                    await con.OpenAsync();
                    SqlTransaction transaction = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"INSERT INTO Review(ReviewId, Rating, Weight, ReviewText, TimeCreated, TimeUpdated, UserId, BoardGameId)
                                        values(@ReviewId,@Rating,@Weight,@ReviewText,@TimeCreated,@TimeUpdated,@UserId,@BoardGameId)";

                    cmd.Parameters.AddWithValue("@ReviewId", review.ReviewId);
                    cmd.Parameters.AddWithValue("@Rating", review.Rating);
                    cmd.Parameters.AddWithValue("@Weight", review.Weight);
                    cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
                    cmd.Parameters.AddWithValue("@BoardGameId", review.BoardGameId);
                    cmd.Parameters.AddWithValue("@TimeCreated", review.TimeCreated);
                    cmd.Parameters.AddWithValue("@TimeUpdated", review.TimeUpdated);

                    double rating, weight;

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "select AVG(rating) from Review where BoardGameId = @BoardGameId";
                        rating = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "select AVG(weight) from Review where BoardGameId = @BoardGameId";
                        weight = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "update BoardGame set Rating = @NewRating, Weight = @NewWeight where BoardGameId = @BoardGameId";
                        cmd.Parameters.AddWithValue("@NewRating", rating);
                        cmd.Parameters.AddWithValue("@NewWeight", weight);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException e)
                    {
                        transaction.Rollback();
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }

                    transaction.Commit();
                    con.Close();
                    return await GetReviewAsync(review.ReviewId);
                }
            }

        }

        public async Task<Review> UpdateReviewAsync(Guid reviewId, Review review)
        {
            int reviewCheck = await ReviewCheckAsync(reviewId);
            if (reviewCheck == 0)
            {
                throw new Exception("No elements with such ID.");
            }

            SqlConnection con = new SqlConnection(conStr);

            using (con)
            {
                using (SqlCommand cmd = new SqlCommand(@"update Review set Rating = @Rating, Weight = @Weight, ReviewText = @ReviewText,
                                                        TimeUpdated = @TimeUpdated where ReviewId = @ReviewId", con))
                {
                    await con.OpenAsync();
                    SqlTransaction transaction = con.BeginTransaction();
                    cmd.Transaction = transaction;
                    var time = DateTime.UtcNow;

                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                    cmd.Parameters.AddWithValue("@Rating", review.Rating);
                    cmd.Parameters.AddWithValue("@Weight", review.Weight);
                    cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                    cmd.Parameters.AddWithValue("@TimeUpdated", review.TimeUpdated);
                    double rating, weight;


                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "select AVG(rating) from Review where BoardGameId = @BoardGameId";
                        cmd.Parameters.AddWithValue("@BoardGameId", review.BoardGameId);
                        rating = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "select AVG(weight) from Review where BoardGameId = @BoardGameId";
                        weight = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "update BoardGame set Rating = @NewRating, Weight = @NewWeight where BoardGameId = @BoardGameId";
                        cmd.Parameters.AddWithValue("@NewRating", rating);
                        cmd.Parameters.AddWithValue("@NewWeight", weight);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException e)
                    {
                        transaction.Rollback();
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }

                    transaction.Commit();
                    con.Close();
                    return await GetReviewAsync(reviewId);
                }
            }

        }

        public async Task DeleteReviewAsync(Guid reviewId)
        {
            int reviewCheck = await ReviewCheckAsync(reviewId);
            if (reviewCheck == 0)
            {
                throw new Exception("No elements with such ID.");
            }

            SqlConnection con = new SqlConnection(conStr);
            Guid boardGameId;
            using (con)
            {
                using (SqlCommand getBoardGameId = new SqlCommand("select BoardGameId from Review where ReviewId = @RevID", con))
                {
                    await con.OpenAsync();
                    getBoardGameId.Parameters.AddWithValue("@RevID", reviewId);
                    SqlDataReader reader = await getBoardGameId.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        boardGameId = reader.GetGuid(0);
                    }
                    else
                    {
                        return;
                    }
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("delete from Review where ReviewId = @ID", con))
                {
                    await con.OpenAsync();
                    SqlTransaction transaction = con.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@ID", reviewId);

                    double rating, weight;

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "select AVG(rating) from Review where BoardGameId = @BoardGameId";
                        cmd.Parameters.AddWithValue("@BoardGameId", boardGameId);
                        rating = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "select AVG(weight) from Review where BoardGameId = @BoardGameId";
                        weight = (double)await cmd.ExecuteScalarAsync();
                        cmd.CommandText = "update BoardGame set Rating = @NewRating, Weight = @NewWeight where BoardGameId = @BoardGameId";
                        cmd.Parameters.AddWithValue("@NewRating", rating);
                        cmd.Parameters.AddWithValue("@NewWeight", weight);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException e)
                    {
                        transaction.Rollback();
                        throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                    }

                    transaction.Commit();
                    con.Close();
                }
            }
        }

    }
}
