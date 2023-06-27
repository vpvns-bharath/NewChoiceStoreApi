using NewChoiceStoreAPI.Models;
using System.Text.Json;

namespace NewChoiceStoreAPI.Data
{
    public class AddProd
    {
        private readonly DatabaseContext _dbContext;

        public AddProd(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProducts()
        {
            var data = File.ReadAllText("Data/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(data);
            foreach (var product in products)
            {
                _dbContext.Products.Add(new Product
                {
                    ProdId = product.ProdId,
                    Title = product.Title,
                    Category = product.Category,
                    Price = product.Price,
                    Description = product.Description,
                    Discount = product.Discount,
                    Rating = product.Rating,
                    Stock = product.Stock,
                    Image = product.Image,
                    Type = product.Type,
                });
            }

            _dbContext.SaveChanges();
        }
    }
}
