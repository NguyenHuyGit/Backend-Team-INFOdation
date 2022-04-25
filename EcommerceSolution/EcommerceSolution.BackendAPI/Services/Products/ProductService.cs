using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Products;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using EcommerceSolution.BackendAPI.Data.EF;
using EcommerceSolution.BackendAPI.Data.Entities;

namespace EcommerceSolution.BackendAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ESolutionDbContext _context;

        public ProductService(ESolutionDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<ProductVm>> CreateProduct(ProductCreateRequest request, string userCreate)
        {
            var query = from p in _context.Products
                        where p.Status == 0
                        select new { p };

            var product = new Product()
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Description = request.Description,
                UserCreate = userCreate,
                CreateDate = DateTime.Now,
                CategoryId = request.CategoryId
            };

            if (request.Name == null)
                return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn chưa nhập tên sản phấm.");
            else
            {
                var checkName = _context.Products.FirstOrDefault(x => x.Name == request.Name);
                if (checkName != null)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn nhập trùng thông tin sản phẩm.");

                if (request.Quantity < 0)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Số lượng phải là số nguyên dương.");
                if (request.CategoryId == 0)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn chưa chọn loại sản phẩm.");
            }


            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            var productVm = new ProductVm()
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                UserCreate = product.UserCreate,
                CreateDate = product.CreateDate,
                CategoryId = product.CategoryId
            };
            return new ApiSuccessResult<ProductVm>(new ProductVm()
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                UserCreate = product.UserCreate,
                CreateDate = product.CreateDate,
                CategoryId = product.CategoryId
            });

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
                    Description = x.p.Description,
                    CategoryId = x.p.CategoryId,
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

        public async Task<ApiResult<ProductUpdateVm>> UpdateProductById(ProductUpdate request , string UserUpdate)
        {
            //find product by ID
            var Product = _context.Products.SingleOrDefault(c=>c.Id==request.Id);
            //check exist
            var NameProduct = _context.Products.FirstOrDefault(x => x.Name == request.Name);
            //check validate
            if(request.Name==null || request.Name=="" )
            {
                return new ApiErrorResult<ProductUpdateVm>("Cập nhật thất bại,mời nhập tên sản phẩm");
            }
            if (request.Quantity <=0)
            {
                return new ApiErrorResult<ProductUpdateVm>("Cập nhật thất bại,mời nhập đúng số lượng");
            }
            if (NameProduct != null)
            {
                return new ApiErrorResult<ProductUpdateVm>("cập nhật thất bại , tên đã tồn tại");
            }
            else
            {
                Product.Name = request.Name;
                Product.Quantity = request.Quantity;
                Product.Description = request.Description;
                Product.UserUpdate = UserUpdate;
                Product.UpdateDate = DateTime.Now;
                Product.CategoryId = request.CategoryId;
            }
             await _context.SaveChangesAsync();
            return new ApiSuccessResult<ProductUpdateVm>(new ProductUpdateVm()
            { Id=request.Id,
              Name=request.Name,
              Quantity=request.Quantity,
              Description=request.Description,
              UserUpdate=UserUpdate,
              UpdateDate=DateTime.Now,
              CategoryId = request.CategoryId,
            });

        }


    }
}
