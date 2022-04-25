
using EcommerceSolution.BackendAPI.Services.Products;
using EcommerceSolution.BackendAPI.ViewModels.Products;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpPut("delete/{productId}")]
        public async Task<IActionResult> TempDeleteProduct(int productId)
        {
            var result = await _productService.TempDeleteProduct(productId);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> PermDeleteProduct(int productId)
        {
            var result = await _productService.PermDeleteProduct(productId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userCreate = User.FindFirstValue(ClaimTypes.GivenName);
            var result = await _productService.CreateProduct(request, userCreate);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> ProductUpdate([FromForm]string UserUpdate, ProductUpdate request)
        {
            var n = User.FindFirstValue(ClaimTypes.GivenName);
            var result = await _productService.UpdateProductById(request, n);
            if (result.IsSuccessed)
            {
                return Ok(result);
            }
            else return BadRequest(result); 

        }




    }
}
