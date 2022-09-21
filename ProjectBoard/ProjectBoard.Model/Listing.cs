using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class Listing : IListing
    {

        public Guid ListingId { get; set; }
        public double Price { get; set; }
        public string Condition { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public Guid UserId { get; set; }
        public Guid BoardGameId { get; set; }
        public Guid? OrderId { get; set; }
        public string Username { get; set; }
        public string BoardGameName { get; set; }

        public Listing(Guid listingId, double price, string condition, DateTime timeCreated, DateTime timeUpdated, Guid userId, Guid boardGameId, Guid? orderId, string username, string boardGameName)
        {
            this.ListingId = listingId;
            this.Price = price;
            this.Condition = condition;
            this.TimeCreated = timeCreated;
            this.TimeUpdated = timeUpdated;
            this.UserId = userId;
            this.BoardGameId = boardGameId;
            this.OrderId = orderId;
            this.Username = username;
            this.BoardGameName = boardGameName;
        }
        public Listing()
        {

        }
    }
}
