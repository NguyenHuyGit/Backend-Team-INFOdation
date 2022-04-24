using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Product;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Product
{
    public interface IProductService
    {
        Task<PagedResult<ProductVm>> GetProductList(GetProductListRequest request);
        Task<PagedResult<ProductVm>> GetProductById(int id, GetProductListRequest request);
    }
}