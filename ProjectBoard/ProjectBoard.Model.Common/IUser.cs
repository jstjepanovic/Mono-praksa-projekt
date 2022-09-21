using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model.Common
{
    public interface IUser
    {
        System.Guid UserId { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime TimeUpdated { get; set; }

    }
}
