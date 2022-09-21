using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Model.Common
{
    public interface IOrder
    {
        System.Guid OrderId { get; set; }
        System.Guid UserId { get; set; }
        string DeliveryAddress { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime TimeUpdated { get; set; }


    }
}
