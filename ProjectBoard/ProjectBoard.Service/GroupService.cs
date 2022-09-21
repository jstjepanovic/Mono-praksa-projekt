using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class GroupService : IGroupService
    {
        IGroupRepository GroupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            GroupRepository = groupRepository;
        }

        public async Task<List<Group>> FindGroupsAsync(Paging paging, Sorting sorting)
        {
            return await GroupRepository.FindGroupsAsync(paging, sorting);
        }

        public async Task<Group> GetGroupAsync(Guid groupId)
        {
            return await GroupRepository.GetGroupAsync(groupId);
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            //group.GroupId = Guid.NewGuid();
            //group.TimeCreated = DateTime.Now;
            //group.TimeUpdated = DateTime.Now;
            return await GroupRepository.CreateGroupAsync(group);
        }

        public async Task<Group> UpdateGroupAsync(Guid groupId, Group group)
        {
            group.TimeUpdated = DateTime.Now;
            return await GroupRepository.UpdateGroupAsync(groupId, group);
        }

        public async Task<bool> DeleteGroupAsync(Guid groupId)
        {
            return await GroupRepository.DeleteGroupAsync(groupId);
        }
    }
}
