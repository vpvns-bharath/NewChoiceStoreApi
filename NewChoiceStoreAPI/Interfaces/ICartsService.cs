using Microsoft.AspNetCore.Mvc;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface ICartsService
    {
        public Task<bool> InsertCartItem(int userId, Product product);
        public Task<ActionResult<IEnumerable<Product>>> GetActiveCartItems(int userId);

        public Task<int> GetActiveCartId(int userId);

        public Task<bool> DeleteCartItem(int userId,string prodId);
    }
}
