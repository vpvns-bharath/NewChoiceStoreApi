namespace NewChoiceStoreAPI.Models
{
    public class PrevOrdersResponse
    {
        public String OrderedAt { get; set; }
        public float AmountPaid { get; set; }
        public List<Product> OrderedProducts { get; set; }        
        

    }
}
