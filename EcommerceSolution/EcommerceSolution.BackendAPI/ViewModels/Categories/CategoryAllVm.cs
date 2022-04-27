using EcommerceSolution.BackendAPI.ViewModels.Brands;

namespace EcommerceSolution.BackendAPI.ViewModels.Categories
{
    public class CategoryAllVm
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public BrandVM Brand { get; set; }
    }
}
