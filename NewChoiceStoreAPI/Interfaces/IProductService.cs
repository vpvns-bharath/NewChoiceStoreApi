using Microsoft.AspNetCore.Mvc;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface IProductService
    {
        public Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category);
        public Task<ActionResult<Product>?> GetProductById(string id);
        public Task<ActionResult<IEnumerable<Product>>> GetRelatedProducts(string category,string prodId);
    }
}
