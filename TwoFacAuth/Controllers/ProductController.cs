using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFacAuth.Areas.Identity.Data;
using TwoFacAuth.Model;
using TwoFacAuth.Service;

namespace TwoFacAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //product/all
        //[Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProductAsync());            
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _productService.GetProductAsync(id));
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto product)
        {
            return Ok(await _productService.SaveProductAsync(product));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto product)
        {
            return Ok(await _productService.UpdateProductAsync(product));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Response<List<ProductDto>> response = await _productService.DeleteProductAsync(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
