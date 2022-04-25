using EcommerceSolution.BackendAPI.Data.EF;
using EcommerceSolution.BackendAPI.Data.Entities;
using EcommerceSolution.BackendAPI.Services.Brands;
using EcommerceSolution.BackendAPI.ViewModels.Brands;
using EcommerceSolution.BackendAPI.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ESolutionDbContext _context;

        public CategoryService(ESolutionDbContext context)
        {
            _context = context;
        }


        public async Task<List<CategoryVM>> GetCategoryListByBrand(int BrandId)
        {
            var categories = _context.Categories.Where(c => c.BrandId == BrandId).Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                BrandId = c.BrandId,
            });
            return await categories.ToListAsync();
        }
    }
}
