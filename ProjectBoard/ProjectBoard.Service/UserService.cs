using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Repository;
using ProjectBoard.Repository.Common;
using ProjectBoard.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Service
{
    public class UserService : IUserService
    {
        protected IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task<List<User>> FindUserAsync(Paging paging, Sorting sorting, UserFilter userFilter)
        {
            return await UserRepository.FindUserAsync(paging, sorting, userFilter);
        }
        public async Task<User> GetUserAsync(Guid userId)
        {
            return await UserRepository.GetUserAsync(userId);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            user.UserId = Guid.NewGuid();
            user.TimeCreated = DateTime.UtcNow;
            user.TimeUpdated = DateTime.UtcNow;
            return await UserRepository.CreateUserAsync(user);
        }
        public async Task<User> UpdateUserAsync(Guid userId, User user)
        {
            if (user.TimeUpdated == null || user.TimeUpdated.Equals(DateTime.MinValue))
            {
                user.TimeUpdated = DateTime.UtcNow;
            }
            return await UserRepository.UpdateUserAsync(userId, user);
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await UserRepository.DeleteUserAsync(userId);
        }
    }
}
