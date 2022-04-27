﻿using EcommerceSolution.BackendAPI.Data.EF;
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

        public async Task<List<CategoryAllVm>> GetAllCategory()
        {

            var query = from c in _context.Categories
                        join b in _context.Brands on c.Id equals b.Id into cib
                        from b in cib.DefaultIfEmpty()
                        select c;
            var cateVm = query.Select(x => new CategoryAllVm()
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                BrandId = x.BrandId,
            });
            return await cateVm.ToListAsync();
        }

        public async Task<CategoryVM> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                return null;
            return new CategoryVM()
            {
                Id = categoryId,
                Name = category.Name,
                BrandId = category.BrandId
            };
        }

        public async Task<List<CategoryVM>> GetCategoryListByBrand(int BrandId)
        {
            var categories = _context.Categories.Where(c => c.BrandId == BrandId).Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                BrandId = c.BrandId,
            });
            if (categories == null)
                return new List<CategoryVM>();
            return await categories.ToListAsync();
        }
    }
}
