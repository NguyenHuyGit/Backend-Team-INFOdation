using EcommerceSolution.BackendAPI.Services.Product;
using EcommerceSolution.BackendAPI.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
