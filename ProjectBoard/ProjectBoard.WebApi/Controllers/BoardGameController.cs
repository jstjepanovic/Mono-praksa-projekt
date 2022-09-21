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
using System.Web.Http;

namespace ProjectBoard.WebApi.Controllers
{
    public class BoardGameController : ApiController
    {
        protected IBoardGameService BoardGameService;
        IMapper Mapper;

        public BoardGameController(IBoardGameService boardGameService, IMapper mapper)
        {
            BoardGameService = boardGameService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("find-boardgame")]
        public async Task<HttpResponseMessage> FindBoardGameAsync(int? rpp = null,
                                                                  int? pageNumber = null,
                                                                  string orderBy = "Rating",
                                                                  string sortOrder = "desc",
                                                                  string name = null,
                                                                  int? minPlayers = null,
                                                                  int? maxPlayers = null,
                                                                  int? age = null,
                                                                  double? rating = null,
                                                                  double? weight = null,
                                                                  string publisher = null,
                                                                  DateTime? timeUpdated = null)
        {
            var boardGames = await BoardGameService.FindBoardGameAsync(new Paging(pageNumber, rpp), new Sorting(orderBy, sortOrder), 
                                                                       new BoardGameFilter(name, minPlayers, maxPlayers, age, rating, weight, publisher, timeUpdated));

            if (boardGames.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, boardGames);
            }
            var boardGamesRest = Mapper.Map<List<BoardGame>, List<BoardGameGetRest>>(boardGames);
            return Request.CreateResponse(HttpStatusCode.OK, boardGamesRest);
        }

        [HttpGet]
        [Route("get-boardgame/{boardGameId}")]
        public async Task<HttpResponseMessage> GetBoardGameAsync(Guid boardGameId)
        {
            var boardGame = Mapper.Map<BoardGame, BoardGameGetRest>(await BoardGameService.GetBoardGameAsync(boardGameId));
            return Request.CreateResponse(HttpStatusCode.OK, boardGame);
        }

        [HttpGet]
        [Route("count-boardgame")]
        public async Task<HttpResponseMessage> BoardGameCountAsync()
        {
            var boardGameCount = await BoardGameService.BoardGameCountAsync();
            return Request.CreateResponse(HttpStatusCode.OK, boardGameCount);
        }

        [HttpPost]
        [Route("create-boardgame")]
        public async Task<HttpResponseMessage> CreateBoardGameAsync(BoardGameRest boardGameCreate)
        {
            var boardGame = Mapper.Map<BoardGameRest, BoardGame>(boardGameCreate);
            var newBoardGame = await BoardGameService.CreateBoardGameAsync(boardGame);
            return Request.CreateResponse(HttpStatusCode.OK, newBoardGame);
        }

        [HttpPut]
        [Route("update-boardgame/{boardGameId}")]
        public async Task<HttpResponseMessage> UpdateBoardGameAsync(Guid boardGameId, BoardGameRest boardGameUpdate)
        {
            var boardGame = Mapper.Map<BoardGameRest, BoardGame>(boardGameUpdate);
            await BoardGameService.UpdateBoardGameAsync(boardGameId, boardGame);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("delete-boardgame/{boardGameId}")]
        public async Task<HttpResponseMessage> DeleteBoardGameAsync(Guid boardGameId)
        {
            await BoardGameService.DeleteBoardGameAsync(boardGameId);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
