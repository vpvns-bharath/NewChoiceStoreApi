using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Services
{
    public class CartsService:ICartsService
    {
        private readonly DatabaseContext _context;
        public CartsService(DatabaseContext context) 
        {
            _context = context;
        }

        public async Task<int> GetActiveCartId(int userId)
        {
            return await _context.Carts.Where(cart=> cart.UserId==userId && cart.IsOrdered==false).Select(cart=>cart.CartId).FirstAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetActiveCartItems(int userId)
        {
            var activeCartItems=new List<Product>();
            bool isCartPresent = await _context.Carts.AnyAsync(cart => cart.UserId == userId && cart.IsOrdered == false);
            if (isCartPresent)
            {
                int cartId = await _context.Carts.Where(cart => cart.UserId == userId && cart.IsOrdered == false).Select(cart=> cart.CartId).FirstAsync();
                activeCartItems = await (from ct in _context.CartItems where ct.CartId == cartId join p in _context.Products on ct.Product.Id equals p.Id select p).ToListAsync();
            }
            return activeCartItems;
        }

        public async Task<bool> InsertCartItem(int userId, Product product)
        {
            bool isCartPresent = await _context.Carts.AnyAsync(cart=> cart.UserId == userId && cart.IsOrdered == false);
            if(!isCartPresent) 
            {
                await _context.Carts.AddAsync(new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>(),
                    IsOrdered = false,
                    OrderedOn = ""
                }) ;
                await _context.SaveChangesAsync();
            }
            
            int cartId = await _context.Carts.Where(cart => cart.UserId==userId && cart.IsOrdered==false).Select(cart=> cart.CartId).FirstOrDefaultAsync();
            var cart =  _context.Carts.Where(cart=> cart.CartId==cartId).First();
            cart.CartItems.Add(new CartItem
            {
                CartId = cartId,
                Product = product
            }) ;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCartItem(int userId,string prodId)
        {
            bool isCartPresent = await _context.Carts.AnyAsync(cart=> cart.UserId==userId && cart.IsOrdered==false);
            if(isCartPresent)
            {
                int cartId = await _context.Carts.Where(cart=> cart.UserId==userId && cart.IsOrdered==false).Select(cart=>cart.CartId).FirstOrDefaultAsync();
                await _context.CartItems.Where(ct => ct.CartId == cartId && ct.Product.ProdId == prodId).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
