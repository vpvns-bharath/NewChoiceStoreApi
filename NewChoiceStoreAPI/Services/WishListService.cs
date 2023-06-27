using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Services
{
    public class WishListService:IWishListService
    {
        private readonly DatabaseContext _databaseContext;
        public WishListService(DatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<Product>> GetActiveWishListItems(int userId)
        {
            List<Product> activeProducts = new List<Product>();
            bool isWishListPresent = await _databaseContext.WishLists.AnyAsync(wl => wl.UserId == userId);
            if (isWishListPresent)
            {
                int wishListId = await _databaseContext.WishLists.Where(wl => wl.UserId == userId).Select(wl => wl.WishListId).FirstAsync();
                activeProducts = await (from wlts in _databaseContext.WishListItems where wlts.WishListId==wishListId
                                        join p in _databaseContext.Products on wlts.Product.Id equals p.Id select p).ToListAsync();
            }
            return activeProducts;
        }

        public async Task<bool> InsertWishListItem(int userId, Product product)
        {
            bool isWishListPresent = await _databaseContext.WishLists.AnyAsync(wl=> wl.UserId==userId);
            if(!isWishListPresent) 
            {
                _databaseContext.WishLists.Add(new WishList
                {
                    UserId = userId,
                    WishListItems = new List<WishListItem>()
                });
                await _databaseContext.SaveChangesAsync();
            }

            int wishListId = await _databaseContext.WishLists.Where(wl=> wl.UserId==userId).Select(wl=> wl.WishListId).FirstAsync();
            var wishList = await _databaseContext.WishLists.Where(wl => wl.WishListId == wishListId).FirstAsync();

            wishList.WishListItems.Add(new WishListItem
            {
                WishListId = wishListId,
                Product = product
            });

            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWishListItem(int userId, string prodId)
        {
            bool isItemPresent = await _databaseContext.WishLists.AnyAsync(wl => wl.UserId == userId);
            if (isItemPresent)
            {
                int wishListId = await _databaseContext.WishLists.Where(wl => wl.UserId == userId).Select(wl => wl.WishListId).FirstOrDefaultAsync();
                await _databaseContext.WishListItems.Where(wlst => wlst.WishListId == wishListId && wlst.Product.ProdId == prodId).ExecuteDeleteAsync();
                await _databaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
