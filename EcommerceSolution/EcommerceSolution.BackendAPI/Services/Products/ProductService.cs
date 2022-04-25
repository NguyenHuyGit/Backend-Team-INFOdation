using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Products;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using EcommerceSolution.BackendAPI.Data.EF;

namespace EcommerceSolution.BackendAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ESolutionDbContext _context;

        public ProductService(ESolutionDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ProductVm>> GetProductList(GetProductListRequest request)
        {
            //Select products
            var query = from p in _context.Products
                        where p.Status == 0
                        select new { p };
            //Search by name
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            //Sort by name or create date

            switch (request.SortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(s => s.p.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(s => s.p.Name);
                    break;
                case "date_asc":
                    query = query.OrderBy(s => s.p.CreateDate);
                    break;
                default:
                    query = query.OrderByDescending(s => s.p.CreateDate);
                    break;
            }
            //Paging
            //Convert Datetime to Epoch timestamp:
            //(int)(x.p.CreateDate - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Quantity = x.p.Quantity,
                    Status = x.p.Status,
                    UserCreate = x.p.UserCreate,
                    CreateDate = x.p.CreateDate,
                    CategoryId = x.p.CategoryId,
                    Description = x.p.Description,
                    UserUpdate = x.p.UserUpdate,
                    UpdateDate = x.p.UpdateDate
                }).ToListAsync();
            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> TempDeleteProduct(int productId)
        {
            var p = _context.Products.Single(s => s.Id == productId);
            p.Status = 1;
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> PermDeleteProduct(int productId)
        {
            var p = _context.Products.Single(s => s.Id == productId);
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}
