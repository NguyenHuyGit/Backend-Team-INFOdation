using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Brands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Brands
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetBrandList();
        Task<BrandVM> GetBrandByCategory(int CategoryId);

    }
}
