using EcommerceSolution.BackendAPI.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{BrandId}")]
        public async Task<IActionResult> GetCategoryListByBrand(int BrandId)
        {
            var _categories = await _categoryService.GetCategoryListByBrand(BrandId);
            return Ok(_categories);
        }
        [HttpGet("name/{categoryId}")]
        public async Task<IActionResult> GetCategoryId(int categoryId)
        {
            var _categories = await _categoryService.GetCategoryById(categoryId);
            return Ok(_categories);
        }
    }
}
