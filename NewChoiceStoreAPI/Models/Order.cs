using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public String PaymentType { get; set; }
        public float AmountPaid { get; set; }
        public String OrderedAt { get; set; }

    }
}
