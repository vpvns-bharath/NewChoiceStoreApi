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
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartService;
        public CartsController(ICartsService cartsService)
        {
            _cartService = cartsService;
        }


        [HttpGet("getActiveCartId/{userId}")]
        public async Task<int> GetCart(int userId)
        {
            return await _cartService.GetActiveCartId(userId);
        }

        [HttpGet("getCartItems/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetActiveCartItems(int userId)
        {
            return await _cartService.GetActiveCartItems(userId);
        }


        [HttpPost("{userId}")]
        public async Task<bool> InsertCartItem(int userId, Product product)
        {
            return await _cartService.InsertCartItem(userId, product);
        }

        [HttpDelete("user/{userId}/{prodId}")]
        public async Task<bool> DeleteCartItem(int userId,string prodId)
        {
            return await _cartService.DeleteCartItem(userId,prodId);
        }
    }
}
