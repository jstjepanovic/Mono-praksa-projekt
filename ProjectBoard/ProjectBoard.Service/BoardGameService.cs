using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository.Common;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class BoardGameService : IBoardGameService
    {
        protected IBoardGameRepository BoardGameRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository)
        {
            BoardGameRepository = boardGameRepository;
        }
        public async Task<List<BoardGame>> FindBoardGameAsync(Paging paging, Sorting sorting, BoardGameFilter filter)
        {
            return await BoardGameRepository.FindBoardGameAsync(paging, sorting, filter);
        }
        public async Task<int> BoardGameCountAsync()
        {
            return await BoardGameRepository.BoardGameCountAsync();
        }
        public async Task<BoardGame> GetBoardGameAsync(Guid boardGameId)
        {
            return await BoardGameRepository.GetBoardGameAsync(boardGameId);
        }
        public async Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame)
        {
            boardGame.TimeCreated = boardGame.TimeUpdated = DateTime.UtcNow;
            boardGame.BoardGameId = Guid.NewGuid();
            return await BoardGameRepository.CreateBoardGameAsync(boardGame);
        }
        public async Task<BoardGame> UpdateBoardGameAsync(Guid boardGameId, BoardGame boardGame)
        {
            boardGame.TimeUpdated = DateTime.UtcNow;
            return await BoardGameRepository.UpdateBoardGameAsync(boardGameId, boardGame);
        }
        public async Task DeleteBoardGameAsync(Guid boardGameId)
        {
            await BoardGameRepository.DeleteBoardGameAsync(boardGameId);
        }
    }
}
