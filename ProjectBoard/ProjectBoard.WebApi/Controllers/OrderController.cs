using AutoMapper;
using ProjectBoard.Common;
using ProjectBoard.Model;
using ProjectBoard.Service;
using ProjectBoard.Service.Common;
using ProjectBoard.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProjectBoard.WebApi.Controllers
{
    public class OrderController : ApiController
    {
        IOrderService OrderService;
        IMapper Mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            OrderService = orderService;
            Mapper = mapper;
        }

        [HttpGet]
        [Route("find-order")]
        public async Task<HttpResponseMessage> FindOrderAsync(Guid? userId=null, int pageNumber=1, int recordsPerPage=5, string orderBy= "TimeCreated", string sortOrder ="ASC", string search=null)
        {
            var paging = new Paging(pageNumber, recordsPerPage);
            var sorting = new Sorting(orderBy, sortOrder);
            var userFilter = new UserFilter(search);
            List<Order> orders = await OrderService.FindOrderAsync(userId, paging, sorting, userFilter);

            if (orders.Count > 0)
            {
                List<OrderRest> listingsRest = Mapper.Map<List<Order>, List<OrderRest>>(orders);
                return Request.CreateResponse(HttpStatusCode.OK, listingsRest);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, orders);
            }
        }

        [HttpGet]
        [Route("get-order")]
        public async Task<HttpResponseMessage> GetOrderAsync(Guid orderId)
        {
            Order order = await OrderService.GetOrderAsync(orderId);
            var orderRest = Mapper.Map<Order, OrderRest>(order);
            if (order == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!"); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, orderRest); 
            }
        }

        [HttpPost]
        [Route("create-order/{listingId}")]
        public async Task<HttpResponseMessage> CreateOrderAsync(Guid listingId, [FromBody] OrderRest orderRest)
        {
            Order order = Mapper.Map<OrderRest, Order>(orderRest);
            var result = await OrderService.CreateOrderAsync(listingId, order);
            if (result == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, orderRest); 
            }
        }

        [HttpPut]
        [Route("update-order")]
        public async Task<HttpResponseMessage> UpdateOrderAsync(Guid orderId, [FromBody] OrderRest orderRest)
        {
            Order order = Mapper.Map<OrderRest, Order>(orderRest);
            var result = await OrderService.UpdateOrderAsync(orderId, order);
            if (result == null)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!"); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, orderRest); 
            }
        }

        [HttpDelete]
        [Route("delete-order")]
        public async Task<HttpResponseMessage> DeleteOrderAsync(Guid orderId)
        {
            var result = await OrderService.DeleteOrderAsync(orderId);
            if (result == false)
            { 
                return Request.CreateResponse(HttpStatusCode.NotFound, "No speciefed object with given Id!"); 
            }
            else
            { 
                return Request.CreateResponse(HttpStatusCode.OK, "Order deleted!");
            }
        }
    }
}