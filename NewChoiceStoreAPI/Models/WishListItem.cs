using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class WishListItem
    {
        [Key]
        public int WishListItemId { get; set; }
        public int WishListId { get; set; }

        public Product Product { get; set; }


    }
}
