using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Product;
using EcommerceSolution.Data.EF;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSolution.BackendAPI.Services.Product
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
                    CreateDate = (int)(x.p.CreateDate - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds
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
    }
}
