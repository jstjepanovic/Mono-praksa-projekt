using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service.Common
{
    public interface IUserService
    {
        Task<List<User>> FindUserAsync(Paging paging, Sorting sorting, UserFilter userFilter);
        Task<User> GetUserAsync(Guid userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(Guid userId, User user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
