using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Services
{
    public class OrderService:IOrderService
    {
        private readonly DatabaseContext _context;
        public OrderService(DatabaseContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<PrevOrdersResponse>> GetPreviousOrders(int userId)
        {
            List<PrevOrdersResponse> result = new List<PrevOrdersResponse>();

            var allOrders = await _context.Orders.Where(order => order.UserId==userId).ToListAsync();
            var carts = (from o in allOrders join c in _context.Carts on o.Cart.CartId equals c.CartId select c).ToList();

            for(int i=0;i<carts.Count;i++)
            {
                var OrderedItems = await (from ct in _context.CartItems
                                          where ct.CartId == carts[i].CartId
                                          join
                                          p in _context.Products on ct.Product.Id equals p.Id
                                          select p).ToListAsync();
                result.Add(new PrevOrdersResponse
                {
                    OrderedAt = allOrders[i].OrderedAt,
                    AmountPaid = allOrders[i].AmountPaid,
                    OrderedProducts = OrderedItems
                });
            }

            return result;
        }

        public async Task<bool> PlaceOrder(int cartId, string amtPaid,string paymentMode, int userId)
        {
            var cart = await _context.Carts.Where(cart => cart.CartId == cartId).FirstAsync();
            cart.IsOrdered = true;
            cart.OrderedOn = DateTime.Now.ToString();
            await _context.Orders.AddAsync(new Order
            {
                Cart = cart,
                UserId = userId,
                PaymentType = paymentMode,
                AmountPaid = float.Parse(amtPaid),
                OrderedAt = DateTime.Now.ToString()
            });

            await _context.SaveChangesAsync();


            return true;
        }
    }
}
