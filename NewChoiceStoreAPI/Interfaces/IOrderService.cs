using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<PrevOrdersResponse>> GetPreviousOrders(int userId);
        public Task<bool> PlaceOrder(int cartId, string amtPaid,string paymentMode, int userId);
    }
}
