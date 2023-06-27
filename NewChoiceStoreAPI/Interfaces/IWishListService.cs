using Microsoft.AspNetCore.Mvc;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface IWishListService
    {
        public Task<IEnumerable<Product>> GetActiveWishListItems(int userId);
        public Task<bool> InsertWishListItem(int userId, Product product);
        public Task<bool> DeleteWishListItem(int userId, string prodId);
    }
}
