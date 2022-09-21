using AutoMapper;
using ProjectBoard.Model;
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
    public class GenreController : ApiController
    {
        IGenreService GenreService;
        IMapper Mapper;
        public GenreController(IGenreService genreService, IMapper mapper)
        {
            this.GenreService = genreService;
            this.Mapper = mapper;
        }
        [HttpGet]
        [Route("getall-genre")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            List<Genre> genres = new List<Genre>();
            genres = await GenreService.GetAllGenreAsync();
            if (genres.Count > 0)
            {
                var genreRest = Mapper.Map<List<Genre>, List<GenreRest>>(genres);
                return Request.CreateResponse(HttpStatusCode.OK, genreRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, genres);
            }
        }
        [HttpGet]
        [Route("get-genre")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] Guid genreId)
        {
            var genre = await GenreService.GetGenreAsync(genreId);
            var genreRest = Mapper.Map<Genre, GenreRest>(genre);
            if (genreRest == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given ID!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, genreRest);
            }
        }
    }
}
