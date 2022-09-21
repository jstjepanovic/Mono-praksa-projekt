using AutoMapper;
using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Service;
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
    public class UserController : ApiController 
    {
        IUserService UserService;
        IMapper Mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("find-user")]
        public async Task<HttpResponseMessage> FindUserAsync(int pageNumber=1 , int recordsPerPage=5, string orderBy="Username" , string sortOrder = "ASC", string nameSearch=null)
        {
            var paging = new Paging(pageNumber, recordsPerPage);
            var sorting = new Sorting(orderBy, sortOrder);
            UserFilter userFilter = new UserFilter(nameSearch);
            List<User> users = await UserService.FindUserAsync(paging, sorting, userFilter);
            if (users.Count > 0)
            {
                List<UserRest> listingsRest = Mapper.Map<List<User>, List<UserRest>>(users);
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, users);
            }
        }


        [HttpGet]
        [Route("get-user/{userId}")]
        public async Task<HttpResponseMessage> GetUserAsync(Guid userId)
        {
            var user = await UserService.GetUserAsync(userId);
            var userRest = Mapper.Map<User, UserRest>(user);
            if (user == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!"); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, userRest); 
            }
        }


        [HttpPost]
        [Route("create-user")]
        public async Task<HttpResponseMessage> CreateUserAsync([FromBody] UserRest userRest)
        {
            User user = Mapper.Map<UserRest, User>(userRest);
            var result = await UserService.CreateUserAsync(user);
            if (result == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, userRest); 
            }
        }


        [HttpPut]
        [Route("update-user/{userId}")]

        public async Task<HttpResponseMessage> UpdateUserAsync(Guid userId, [FromBody] UserRest userRest)
        {
            User user = Mapper.Map<UserRest, User>(userRest);
            var result = await UserService.UpdateUserAsync(userId, user);
            if (result == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!");
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, userRest); 
            }
        }


        [HttpDelete]
        [Route("delete-user/{userId}")]
        public async Task<HttpResponseMessage> DeleteUserAsync(Guid userId)
        {
            var result = await UserService.DeleteUserAsync(userId);
            if (result == false)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!"); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, "User deleted!");
            }
        }
    }
}