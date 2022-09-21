using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.WebApi.Models
{
    public class ListingRest
    {
        public double Price { get; set; }
        public string Condition { get; set; }
        public DateTime TimeCreated { get; set; }
        public Guid UserId { get; set; }
        public Guid BoardGameId { get; set; }
        public string Username { get; set; }
        public string BoardGameName { get; set; }

        public ListingRest()
        {
        }
    }
}