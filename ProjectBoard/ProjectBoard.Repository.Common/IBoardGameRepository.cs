using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IBoardGameRepository
    {
        Task<int> BoardGameCheckAsync(Guid boardGameId);
        Task<List<BoardGame>> FindBoardGameAsync(Paging paging, Sorting sorting, BoardGameFilter filter);
        Task<BoardGame> GetBoardGameAsync(Guid boardGameId);
        Task<int> BoardGameCountAsync();
        Task<BoardGame> CreateBoardGameAsync(BoardGame boardGame);
        Task<BoardGame> UpdateBoardGameAsync(Guid boardGameId, BoardGame boardGame);
        Task DeleteBoardGameAsync(Guid boardGameId);
    }
}
