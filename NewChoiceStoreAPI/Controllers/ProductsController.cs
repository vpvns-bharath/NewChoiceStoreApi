using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Controllers
{
    [Authorize]
    [Route("NewChoiceStoreApi/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IProductService _productService;

        public ProductsController(DatabaseContext context,IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        
        [HttpGet("Category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            return await _productService.GetProductByCategory(category);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<Product>?> GetProductById(string id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpGet("RelatedProducts/{category}/{prodId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetRelatedProducts(string category,string prodId)
        {
            return await _productService.GetRelatedProducts(category,prodId);
        }

    }
}
