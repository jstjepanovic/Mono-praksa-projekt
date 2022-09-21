using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBoard.WebApi.Models
{
    public class OrderRest
    {
        public string DeliveryAddress { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime TimeCreated { get; set; }
        public string BoardGameName { get; set; }
        public OrderRest()
        {
        }
    }
}