using AutoMapper;
using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Service.Common;
using ProjectBoard.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProjectBoard.WebApi.Controllers
{
    public class GroupController:ApiController
    {
        IGroupService GroupService;
        IMapper Mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("find-groups")]
        public async Task<HttpResponseMessage> FindGroupAsync(int pageNumber = 1, int recordsPerPage = 10, string orderBy = "TimeCreated", string sortOrder = "asc")
        {
            var paging = new Paging(pageNumber, recordsPerPage);
            var sorting = new Sorting(orderBy, sortOrder);
            List<Group> groups = await GroupService.FindGroupsAsync(paging, sorting);
            List<GroupRest> groupsRest = Mapper.Map<List<Group>, List<GroupRest>>(groups);
            return Request.CreateResponse(HttpStatusCode.OK, groupsRest);
        }

        [HttpGet]
        [Route("get-group")]
        public async Task<HttpResponseMessage> GetGroupAsync([FromUri] Guid groupId)
        {
            var group = await GroupService.GetGroupAsync(groupId);
            var groupRest = Mapper.Map<Group, GroupRest>(group);
            return Request.CreateResponse(HttpStatusCode.OK, groupRest);

        }

        [HttpPost]
        [Route("create-group")]
        public async Task<HttpResponseMessage> CreateGroupAsync([FromBody] Group group)
        {
            var createdGroup = await GroupService.CreateGroupAsync(group);
            var groupRest = Mapper.Map<Group, GroupRest>(createdGroup);
            return Request.CreateResponse(HttpStatusCode.OK, createdGroup);
        }

        [HttpPut]
        [Route("update-group")]
        public async Task<HttpResponseMessage> UpdateGroupAsync([FromBody] Guid groupId, Group group)
        {
            var result = await GroupService.UpdateGroupAsync(groupId, group);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("delete-group")]
        public async Task<HttpResponseMessage> DeleteGroupAsync([FromUri] Guid groupId)
        {
            bool isDeleted = await GroupService.DeleteGroupAsync(groupId);
            if (isDeleted == false)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, isDeleted);
        }
    }
}