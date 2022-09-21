using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IGroupRepository
    {
        Task<List<Group>> FindGroupsAsync(Paging paging, Sorting sorting);
        Task<Group> GetGroupAsync(Guid groupId);
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Guid groupId, Group group);
        Task<bool> DeleteGroupAsync(Guid groupId);


    }
}
