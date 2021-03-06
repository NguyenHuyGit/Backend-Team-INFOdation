using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Products;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductVm>> GetProductList(GetProductListRequest request);

        Task<ApiResult<bool>> TempDeleteProduct(int productId);
        Task<ApiResult<bool>> PermDeleteProduct(int productId);

        Task<ApiResult<ProductVm>> CreateProduct(ProductCreateRequest request, string userCreate);
        Task<ApiResult<ProductUpdateVm>> UpdateProductById (ProductUpdate productUpdate , string UserUpdate);
        Task<ProductDetails> GetProductDetails(int productId);
    }
}
