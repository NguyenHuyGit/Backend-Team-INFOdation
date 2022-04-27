using EcommerceSolution.BackendAPI.Services.Brands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var _brands = await _brandService.GetBrandList();
            return Ok(_brands);
        }
        
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetBrandByCategory(int CategoryId)
        {
            var brand = await _brandService.GetBrandByCategory(CategoryId);
            return Ok(brand);
        }
        [HttpGet("name/{brandId}")]
        public async Task<IActionResult> GetBrandById(int brandId)
        {
            var brand = await _brandService.GetBrandById(brandId);
            return Ok(brand);
        }

    }
}
