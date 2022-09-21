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
    public class OrderService : IOrderService
    {

        protected IOrderRepository OrderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        public async Task<List<Order>> FindOrderAsync(Guid? userId, Paging paging, Sorting sorting, UserFilter userFilter)
        {
            return await OrderRepository.FindOrderAsync(userId, paging, sorting, userFilter);
        }
        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await OrderRepository.GetOrderAsync(orderId);
        }
        public async Task<Order> CreateOrderAsync(Guid listingId, Order order)
        {
            order.OrderId = Guid.NewGuid();
            order.TimeCreated = DateTime.UtcNow;
            order.TimeUpdated = DateTime.UtcNow;
            return await OrderRepository.CreateOrderAsync(listingId, order);
        }
        public async Task<Order> UpdateOrderAsync(Guid orderId, Order order)
        {
            order.TimeUpdated = DateTime.UtcNow;
            return await OrderRepository.UpdateOrderAsync(orderId, order);
        }
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await OrderRepository.DeleteOrderAsync(orderId);
        }


    }
}
