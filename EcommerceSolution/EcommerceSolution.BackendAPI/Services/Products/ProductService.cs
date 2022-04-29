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
using System.Text;

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
            string specialChar = @"\/|!#$%&/()=?»«£§€{}.-;'<>,*^+~`:[]" + '"';
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
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(request.Name))
                errors.Add("Thêm mới không thành công. Bạn chưa nhập tên sản phẩm.");
            else
            {
                var checkSpecialChar = hasSpecialChar(request.Name);
                if (checkSpecialChar)
                    errors.Add("Thêm mới không thành công. Bạn nhập ký tự đặc biệt ngoài @ _.");

                var checkName = _context.Products.FirstOrDefault(x => x.Name == request.Name);
                if (checkName != null)
                    errors.Add("Thêm mới không thành công. Bạn nhập trùng tên sản phẩm.");
            }
                

                if (request.Quantity < 0)
                    errors.Add("Thêm mới không thành công. Số lượng phải là số nguyên dương.");

                if (request.BrandId <= 0)
                    errors.Add("Thêm mới không thành công. Bạn chưa chọn hãng.");

                if (request.CategoryId <= 0)
                    errors.Add("Thêm mới không thành công. Bạn chưa chọn loại sản phẩm.");
                else
                {
                    var checkCateInBrand = CheckCategoriesInBrand(request.BrandId, request.CategoryId);
                    if (!checkCateInBrand)
                        errors.Add("Thêm mới không thành công. Loại sản phẩm và Hãng không trùng khớp.");
                }
            
            String[] str = errors.ToArray();
            if (str.Length > 0)
                return new ApiValidationErrors<ProductVm>(str);
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
        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
        public async Task<PagedResult<ProductVm>> GetProductList(GetProductListRequest request)
        {
            //Select products
            List<Product> query = await (from p in _context.Products
                        where p.Status == 0
                        select p ).ToListAsync();
            //Search by name
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var checkSpecialChar = hasSpecialChar(request.Keyword);
                if (checkSpecialChar)
                {
                    return new PagedResult<ProductVm>()
                    {
                        TotalRecords = 0,
                        PageIndex = 1,
                        PageSize = 1,
                        Message = "Không tìm thấy sản phẩm"
                    };
                }; 
                query = query.FindAll(delegate (Product p)
                {
                    if (ConvertToUnSign(p.Name.ToLower()).Contains(ConvertToUnSign(request.Keyword.ToLower())))
                        return true;
                    else
                        return false;
                });
            }
                //query = query.Where(x => x.p.Name.Contains(request.Keyword));
            //Sort by name or create date

            switch (request.SortOrder)
            {
                case "name_asc":
                    query = query.OrderBy(s => s.Name).ToList();
                    break;
                case "name_desc":
                    query = query.OrderByDescending(s => s.Name).ToList();
                    break;
                case "date_asc":
                    query = query.OrderBy(s => s.CreateDate).ToList();
                    break;
                default:
                    query = query.OrderByDescending(s => s.CreateDate).ToList();
                    break;
            }
            //Paging
            //Convert Datetime to Epoch timestamp:
            //(int)(x.p.CreateDate - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds
            string mess = "";
            int totalRow = query.Count();
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
            
            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Status = x.Status,
                    UserCreate = x.UserCreate,
                    CreateDate = x.CreateDate,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    BrandId = x.BrandId,
                    UserUpdate = x.UserUpdate,
                    UpdateDate = x.UpdateDate
                });

            var pagedResult = new PagedResult<ProductVm>()
            {
                TotalRecords = totalRow,
                Items = data.ToList(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Message = mess
            };
            return pagedResult;
        }


        public async Task<ApiResult<bool>> TempDeleteProduct(int productId)
        {
            var p = await _context.Products.FindAsync(productId);
            if(p == null)
                return new ApiMessageResult<bool>(false, $"Không tìm thấy sản phẩm với ID {productId}");
            p.Status = 1;
            await _context.SaveChangesAsync();
            return new ApiMessageResult<bool>(true, "Xóa tạm thời thành công");
        }

        public async Task<ApiResult<bool>> PermDeleteProduct(int productId)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null)
                return new ApiMessageResult<bool>(false, $"Không tìm thấy sản phẩm với ID {productId}");
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
            return new ApiMessageResult<bool>(true, "Xóa thành công");
        }

        public async Task<ApiResult<ProductUpdateVm>> UpdateProductById(ProductUpdate request, string UserUpdate, int id)
        {
            List<string> errors = new List<string>();
            //find product by ID
            var Product = await _context.Products.FindAsync(id);
            if(Product == null)
                errors.Add($"Cập nhật không thành công, không tìm thấy sản phẩm với ID {id}");
            //check exist
            var NameProduct = _context.Products.FirstOrDefault(x => x.Name == request.Name);
            //check validate
            if (string.IsNullOrEmpty(request.Name))
            {
                errors.Add("Cập nhật không thành công,mời nhập tên sản phẩm");
            }
            else
            {
                var checkSpecialChar = hasSpecialChar(request.Name);
                if (checkSpecialChar)
                    errors.Add("Cập nhật không thành công. Bạn nhập ký tự đặc biệt ngoài @ _.");
            }
            if (request.Quantity < 0)
            {
                errors.Add("Cập nhật không thành công,mời nhập đúng số lượng");
            }
            if (NameProduct != null && NameProduct.Id != Product.Id)
            {
                errors.Add("Cập nhật không thành công, tên đã tồn tại");
            }
            if (request.BrandId <= 0)
                errors.Add("Cập nhật không thành công. Bạn chưa chọn hãng.");

            if (request.CategoryId <= 0)
                errors.Add("Cập nhật không thành công. Bạn chưa chọn loại sản phẩm.");
            else
            {
                var checkCateInBrand = CheckCategoriesInBrand(request.BrandId, request.CategoryId);
                if (!checkCateInBrand)
                    errors.Add("Cập nhật không thành công. Loại sản phẩm và Hãng không trùng khớp.");
            }
            String[] str = errors.ToArray();
            //If errors have any error then return:
            if (errors.Count > 0)
                return new ApiValidationErrors<ProductUpdateVm>(str);
            //If errors is empty then update product
            Product.Name = request.Name;
            Product.Quantity = request.Quantity;
            Product.Description = request.Description;
            Product.UserUpdate = UserUpdate;
            Product.UpdateDate = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(7));
            Product.CategoryId = request.CategoryId;
            Product.BrandId = request.BrandId;
            
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
