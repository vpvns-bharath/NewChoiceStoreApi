using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public int CartId { get; set; }

        public Product Product { get; set; }
    }
}
