using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Products;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductVm>> GetProductList(GetProductListRequest request);

    }
}
