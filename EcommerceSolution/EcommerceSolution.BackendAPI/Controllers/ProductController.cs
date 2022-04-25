
using EcommerceSolution.BackendAPI.Services.Products;
using EcommerceSolution.BackendAPI.ViewModels.Products;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList([FromQuery] GetProductListRequest request)
        {
            var result = await _productService.GetProductList(request);
            return Ok(result);
        }
        [HttpPut("/delete/{productId}")]
        public async Task<IActionResult> TempDeleteProduct(int productId)
        {
            var result = await _productService.TempDeleteProduct(productId);
            return Ok(result);
        }

        [HttpDelete("/delete/{productId}")]
        public async Task<IActionResult> PermDeleteProduct(int productId)
        {
            var result = await _productService.PermDeleteProduct(productId);
            return Ok(result);
        }
    }
}
