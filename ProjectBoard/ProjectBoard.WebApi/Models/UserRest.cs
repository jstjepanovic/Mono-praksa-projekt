﻿using System;

namespace ProjectBoard.WebApi.Models
{
    public class UserRest
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRest()
        {
        }
    }
}