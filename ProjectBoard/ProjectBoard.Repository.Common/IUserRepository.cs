



using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IUserRepository
    {
        Task<List<User>> FindUserAsync(Paging paging, Sorting sorting, UserFilter userFilter);
        Task<User> GetUserAsync(Guid userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(Guid userId, User user);
        Task<bool> DeleteUserAsync(Guid userId);

    }
}
