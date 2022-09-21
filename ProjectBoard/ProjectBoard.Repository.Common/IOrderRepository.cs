using ProjectBoard.Common;
using ProjectBoard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBoard.Repository.Common
{
    public interface IOrderRepository
    {
        Task<List<Order>> FindOrderAsync(Guid? userId,Paging paging, Sorting sorting, UserFilter userFilter);
        Task<Order> GetOrderAsync(Guid orderId);
        Task<Order> CreateOrderAsync(Guid listingId, Order order);
        Task<Order> UpdateOrderAsync(Guid orderId, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);

    }
}
