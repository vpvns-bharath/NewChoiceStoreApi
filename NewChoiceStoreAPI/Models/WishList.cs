using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class WishList
    {
        [Key]
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public List<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
    }
}
