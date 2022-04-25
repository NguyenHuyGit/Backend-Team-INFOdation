using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.Data.EF;
using EcommerceSolution.BackendAPI.ViewModels.Brands;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly ESolutionDbContext _context;
        public BrandService(ESolutionDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrandVM>> GetBrandList()
        {
            var brands = _context.Brands.Select(b => new BrandVM
            {
                Id = b.Id,
                Name = b.Name,
            });
            return await brands.ToListAsync();
        }

        public async Task<BrandVM> GetBrandByCategory(int CategoryId)
        {
            var category = await _context.Categories.FindAsync(CategoryId);
            var brand = await _context.Brands.FindAsync(category.BrandId);
            var brandVM = new BrandVM()
            {
                Id = brand.Id,
                Name = brand.Name
            };
            return brandVM;
        }
    }
}
