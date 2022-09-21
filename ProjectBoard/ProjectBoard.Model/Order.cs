using ProjectBoard.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model
{
    public class Order : IOrder
    {
        public System.Guid OrderId { get; set; }
        public System.Guid UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public string BoardGameName { get; set; }
        public Order() 
        {
        }
        public Order(System.Guid orderId, string deliveryAddress, DateTime timeCreated, DateTime timeUpdated, System.Guid userId, string boardGameName)
        {
            OrderId = orderId;
            DeliveryAddress = deliveryAddress;
            TimeCreated = timeCreated;
            TimeUpdated = timeUpdated;
            UserId = userId;
            BoardGameName = boardGameName;
        }
    }
}
