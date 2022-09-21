using ProjectBoard.Model.Common;
using System;

namespace ProjectBoard.Model
{
    public class User : IUser
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public User()
        {
        }
        public User(Guid userId, string name, string email, string password, DateTime timeCreated, DateTime timeUpdated)
        {
            UserId = userId;
            Username = name;
            Email = email;
            Password = password;
            TimeCreated = timeCreated;
            TimeUpdated = timeUpdated;
        }
    }
}
