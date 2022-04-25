
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGetProductById([FromQuery]int id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }
    }
}
