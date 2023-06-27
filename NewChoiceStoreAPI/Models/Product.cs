using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? ProdId { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }
        public string? Discount { get; set; }
        public string? Rating { get; set; }
        public string? Stock { get; set; }
        public string? Image { get; set;}
        public string? Type { get; set; }

    }
}
