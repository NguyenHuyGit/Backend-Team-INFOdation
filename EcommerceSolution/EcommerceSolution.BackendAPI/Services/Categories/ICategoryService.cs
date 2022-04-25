using EcommerceSolution.BackendAPI.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetCategoryListByBrand(int BrandId);
    }
}
