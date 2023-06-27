using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Controllers
{
    [Route("NewChoiceStoreApi/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<PrevOrdersResponse>> GetPreviousOrders(int userId)
        {
            return await _orderService.GetPreviousOrders(userId);
        }

        [HttpPost("{cartId}/{amtPaid}/{paymentMode}")]
        public async Task<bool> PlaceOrder(int cartId,string amtPaid,string paymentMode,[FromBody]int userId)
        {
            return await _orderService.PlaceOrder(cartId, amtPaid, paymentMode,userId);
        }

    }
}
