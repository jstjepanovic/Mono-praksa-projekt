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
    public class GroupRepository : IGroupRepository
    {
        private static string conStr = Environment.GetEnvironmentVariable("ConnectionString");

        private async Task<bool> GroupIdExists(Guid groupId)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                try
                {
                    string checkText = "Select Count (*) FROM TableGroup WHERE GroupId=@groupId;";
                    SqlCommand checkIdCommand = new SqlCommand(checkText, connection);
                    checkIdCommand.Parameters.AddWithValue("@groupId", groupId);
                    if ((int)await checkIdCommand.ExecuteScalarAsync() > 0)
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

        public async Task<List<Group>> FindGroupsAsync(Paging paging, Sorting sorting)
        {
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand findCommand = new SqlCommand("", connection);
                List<Group> groups = new List<Group>();
                int? offset = (paging.PageNumber - 1) * paging.RecordsByPage;
                StringBuilder stringBuilder = new StringBuilder(@"SELECT g.Name as GroupName, g.CategoryId,g.TimeCreated, g.TimeUpdated, c.Name 
                From TableGroup g join Category c on(g.CategoryId=c.CategoryId)
                WHERE 1=1 ");
                stringBuilder.Append(string.Format("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder));
                stringBuilder.Append("offset @offset rows fetch next @recordsByPage rows only;");

                findCommand.Parameters.AddWithValue("@offset", offset);
                findCommand.Parameters.AddWithValue("@recordsByPage", paging.RecordsByPage);
                findCommand.CommandText = stringBuilder.ToString();
                SqlDataReader reader = await findCommand.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        groups.Add(new Group(reader.GetString(0), reader.GetGuid(1), reader.GetString(4)));
                    }
                }
                else
                {
                    throw new Exception("Group doesn't exist.");
                }
                return groups;
            }
        }

        public async Task<Group> GetGroupAsync(Guid groupId)
        {
            if (await GroupIdExists(groupId) == false)
            {
                throw new Exception("No group with given id");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                SqlCommand getCommand = new SqlCommand("SELECT * FROM tableGroup WHERE GroupId = @groupId;", connection);
                getCommand.Parameters.AddWithValue("@groupId", groupId);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await getCommand.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        Group group = new Group( reader.GetString(1), reader.GetGuid(4), reader.GetString(1));
                        group.GroupId = Guid.NewGuid();
                        group.TimeCreated = DateTime.Now;
                        group.TimeUpdated = DateTime.Now;
                        return group;
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

        public async Task<Group> CreateGroupAsync(Group group)
        {
            //if(await GroupIdExists(group.GroupId) == true)
            //{
            //    throw new Exception("Group already exists!");
            //}

            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand createCommand = new SqlCommand("INSERT INTO TableGroup (GroupId, Name, CategoryId, TimeCreated, TimeUpdated) Values(@groupId, @name, @categoryId, @timeCreated, @timeUpdated);", connection);
                createCommand.Parameters.AddWithValue("@groupId", group.GroupId);
                createCommand.Parameters.AddWithValue("@name", group.Name);
                createCommand.Parameters.AddWithValue("@categoryId", group.CategoryId);
                createCommand.Parameters.AddWithValue("@timeCreated", group.TimeCreated);
                createCommand.Parameters.AddWithValue("@timeUpdated", group.TimeUpdated);

                try
                {
                    await createCommand.ExecuteNonQueryAsync();

                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }

                return await GetGroupAsync(group.GroupId);

            }
        }

        public async Task<Group> UpdateGroupAsync(Guid groupId, Group group)
        {
            if(await GroupIdExists(groupId) == false)
            {
                throw new Exception("no group with given ID");
            }

            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                string update = "Update TableGroup Set Name=@name, TimeUpdated=@timeUpdated, CategoryId=@categoryId Where GroupId=@groupId";
                SqlCommand updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@name", group.Name);
                updateCommand.Parameters.AddWithValue("@timeUpdated", group.TimeUpdated);
                updateCommand.Parameters.AddWithValue("@categoryId", group.CategoryId);
                try
                {
                    await updateCommand.ExecuteNonQueryAsync();
                    return await GetGroupAsync(groupId);
                }
                catch(SqlException e)
                {
                    throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                }
            }
        }

        public async Task<bool> DeleteGroupAsync(Guid groupId)
        {
            if(await GroupIdExists(groupId) == false)
            {
                throw new Exception("No group with given Id!");
            }
            SqlConnection connection = new SqlConnection(conStr);
            using (connection)
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(
                "DELETE FROM TableGroup WHERE GroupId = @groupId;", connection);
                command.Parameters.AddWithValue("@groupId", groupId);

            
            try
            {
                await command.ExecuteNonQueryAsync();
                return true;
            }

            catch(SqlException e)
            {
                throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
            }
            }
        }

    }
}
