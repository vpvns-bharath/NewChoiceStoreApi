using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Services
{
    public class ProductService:IProductService
    {
        private readonly DatabaseContext _context;
        public ProductService(DatabaseContext context) 
        {
            _context= context;
        }
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products= await _context.Products.ToListAsync();
            var reqProducts = new List<Product>();
            foreach (var product in products) 
            {
                if(product.Category == category)
                {
                    reqProducts.Add(product);
                }
            }
            return reqProducts;
        }

        public async Task<ActionResult<Product>?> GetProductById(string id)
        {
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                if (product.ProdId == id)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetRelatedProducts(string category,string prodId)
        {
            var relatedProducts = new List<Product>();
            await _context.Products.Where(product=> product.ProdId!=prodId && product.Category== category).ForEachAsync(product => { relatedProducts.Add(product); });
            return relatedProducts;
        }
    }
}
