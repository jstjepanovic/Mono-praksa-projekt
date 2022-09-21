using AutoMapper;
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
using System.Web.Http;

namespace ProjectBoard.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        ICategoryService CategoryService;
        IMapper Mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.CategoryService = categoryService;
            this.Mapper = mapper;
        }
        [HttpGet]
        [Route("getall-category")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Category> categorys = new List<Category>();
            categorys = await CategoryService.GetAllCategoryAsync();
            if (categorys.Count > 0)
            {
                var categoryRest = Mapper.Map<List<Category>, List<CategoryRest>>(categorys);
                return Request.CreateResponse(HttpStatusCode.OK, categoryRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, categorys);
            }
        }
        [HttpGet]
        [Route("get-category")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid categoryId)
        {
            var category = await CategoryService.GetCategoryAsync(categoryId);
            var categoryRest = Mapper.Map<Category, CategoryRest>(category);
            if (categoryRest != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, categoryRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
        }
    }
}
