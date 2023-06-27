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
    public class WishListsController : ControllerBase
    {
        private readonly IWishListService _wishListService;

        public WishListsController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<Product>>GetActiveWishListItems(int userId)
        {
            return await _wishListService.GetActiveWishListItems(userId);
        }

        [HttpPost("{userId}")]
        public async Task<bool> InsertWishListItem(int userId,[FromBody] Product product)
        {
            return await _wishListService.InsertWishListItem(userId, product);
        }

        [HttpDelete("{userId}/{prodId}")]
        public async Task<bool> DeleteWishListItem(int userId,string prodId)
        {
            return await _wishListService.DeleteWishListItem(userId, prodId);
        }

    }        
}
