using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Products;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using EcommerceSolution.BackendAPI.Data.EF;
using EcommerceSolution.BackendAPI.Data.Entities;
using System.Text.RegularExpressions;
using EcommerceSolution.BackendAPI.ViewModels.Categories;
using System.Collections.Generic;

namespace EcommerceSolution.BackendAPI.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ESolutionDbContext _context;

        public ProductService(ESolutionDbContext context)
        {
            _context = context;
        }
        private static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«£§€{}.-;'<>,*";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        private bool CheckCategoriesInBrand(int BrandId, int CategoryId)
        {
            var categories = _context.Categories.Where(c => c.BrandId == BrandId && c.Id == CategoryId).Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                BrandId = c.BrandId,
            });
            if (categories == null)
                return false;
            foreach (var category in categories)
            {
                if(category.BrandId == BrandId)
                    return true;
            }
            return false;

        }
        public async Task<ApiResult<ProductVm>> CreateProduct(ProductCreateRequest request, string userCreate)
        {

            if (request.Name == null)
                return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn chưa nhập tên sản phấm.");
            else
            {
                var checkSpecialChar = hasSpecialChar(request.Name);
                if(checkSpecialChar)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn nhập ký tự đặc biệt ngoài @ _.");
                var checkName = _context.Products.FirstOrDefault(x => x.Name == request.Name);
                if (checkName != null)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn nhập trùng thông tin sản phẩm.");

                if (request.Quantity < 0)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Số lượng phải là số nguyên dương.");
                if (request.BrandId == 0)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn chưa chọn hãng.");
                if (request.CategoryId == 0)
                    return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Bạn chưa chọn loại sản phẩm.");
                else
                {
                    var checkCateInBrand = CheckCategoriesInBrand(request.BrandId, request.CategoryId);
                    if (!checkCateInBrand)
                        return new ApiErrorResult<ProductVm>("Thêm mới không thành công. Loại sản phẩm và Hãng không trùng khớp.");
                }

            }

            var product = new Product()
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Description = request.Description,
                UserCreate = userCreate,
                CreateDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(7)),
                CategoryId = request.CategoryId,
                BrandId = request.BrandId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return new ApiSuccessResult<ProductVm>(new ProductVm()
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                UserCreate = product.UserCreate,
                CreateDate = product.CreateDate,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId
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
            string mess = "";
            int totalRow = await query.CountAsync();
            if(totalRow > 0)
            {
                if (request.PageSize == 0)
                    request.PageSize = totalRow;
            }
            else
            {
                request.PageSize = 1;
                mess = "Không tìm thấy sản phẩm";
            }
            
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
                    BrandId = x.p.BrandId,
                    UserUpdate = x.p.UserUpdate,
                    UpdateDate = x.p.UpdateDate
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Message = mess
            };
            return pagedResult;
        }


        public async Task<ApiResult<bool>> TempDeleteProduct(int productId)
        {
            var p = _context.Products.Single(s => s.Id == productId);
            p.Status = 1;
            await _context.SaveChangesAsync();
            return new ApiMessageResult<bool>(true, "Xóa tạm thời thành công");
        }

        public async Task<ApiResult<bool>> PermDeleteProduct(int productId)
        {
            var p = _context.Products.Single(s => s.Id == productId);
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
            return new ApiMessageResult<bool>(true, "Xóa thành công");
        }

        public async Task<ApiResult<ProductUpdateVm>> UpdateProductById(ProductUpdate request, string UserUpdate, int id)
        {
            //find product by ID
            var Product = _context.Products.SingleOrDefault(c => c.Id == id);
            //check exist
            var NameProduct = _context.Products.FirstOrDefault(x => x.Name == request.Name);
            //check validate
            if (request.Name == null)
            {
                return new ApiErrorResult<ProductUpdateVm>("Cập nhật thất bại,mời nhập tên sản phẩm");
            }
            else
            {
                var checkSpecialChar = hasSpecialChar(request.Name);
                if (checkSpecialChar)
                    return new ApiErrorResult<ProductUpdateVm>("Thêm mới không thành công. Bạn nhập ký tự đặc biệt ngoài @ _.");
            }
            if (request.Quantity < 0)
            {
                return new ApiErrorResult<ProductUpdateVm>("Cập nhật thất bại,mời nhập đúng số lượng");
            }
            if (NameProduct != null && NameProduct.Id != Product.Id)
            {
                return new ApiErrorResult<ProductUpdateVm>("cập nhật thất bại , tên đã tồn tại");
            }
            else
            {
                Product.Name = request.Name;
                Product.Quantity = request.Quantity;
                Product.Description = request.Description;
                Product.UserUpdate = UserUpdate;
                Product.UpdateDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(7));
                Product.CategoryId = request.CategoryId;
                Product.BrandId = request.BrandId;
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<ProductUpdateVm>(new ProductUpdateVm()
            {
                Id = id,
                Name = request.Name,
                Quantity = request.Quantity,
                Description = request.Description,
                UserUpdate = UserUpdate,
                UpdateDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(7)),
                CategoryId = request.CategoryId,
                BrandId = request.BrandId,
            });

        }
        public async Task<ApiResult<ProductDetails>> GetProductDetails(int productId)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null)
                return new ApiErrorResult<ProductDetails>($"Không tìm thấy sản phẩm với ID: {productId}");
            var categoryId = p.CategoryId;
            var c = _context.Categories.Single(c => c.Id == categoryId);
            var brandId = p.BrandId;
            var b = _context.Brands.Single(c => c.Id == brandId);
            var detailProduct = new ProductDetails();
            detailProduct.Name = p.Name;
            detailProduct.Quantity = p.Quantity;
            //if (p.Description == null)
            //    detailProduct.Description = "Không có thông tin";
            //else
                detailProduct.Description = p.Description;
            detailProduct.brandName = b.Name;
            detailProduct.categoryName = c.Name;
            detailProduct.UserCreate = p.UserCreate;
            detailProduct.CreateDate = p.CreateDate;
            //detailProduct.CreateDate = (p.CreateDate - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds.ToString();
            //if (p.UserUpdate == null)
            //    detailProduct.userUpdate = "Không có thông tin";
            //else
                detailProduct.userUpdate = p.UserUpdate;
            //if(p.UpdateDate == null)
            //    detailProduct.updateDate = "Không có thông tin";
            //else
            //{
            //    DateTime temp = p.UpdateDate.Value;
            //    detailProduct.updateDate = (temp - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds.ToString();
            //}
            detailProduct.updateDate = p.UpdateDate;
            detailProduct.Status = p.Status;
            return new ApiSuccessResult<ProductDetails>(detailProduct);
        }
    }
}
