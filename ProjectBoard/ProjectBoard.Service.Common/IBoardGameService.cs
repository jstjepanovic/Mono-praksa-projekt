using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Service.Common
{
    public interface IBoardGameService
    {
        Task<List<BoardGame>> FindBoardGameAsync(Paging paging, Sorting sorting, BoardGameFilter filter);
        Task<BoardGame> GetBoardGameAsync(Guid boardGameId);
        Task<int> BoardGameCountAsync();
        Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame);
        Task<BoardGame> UpdateBoardGameAsync(Guid boardGameId, BoardGame boardGame);
        Task DeleteBoardGameAsync(Guid boardGameId);
    }
}
