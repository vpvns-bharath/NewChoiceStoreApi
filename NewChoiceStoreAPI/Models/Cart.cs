using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public bool IsOrdered { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string OrderedOn { get; set; }
    }
}
