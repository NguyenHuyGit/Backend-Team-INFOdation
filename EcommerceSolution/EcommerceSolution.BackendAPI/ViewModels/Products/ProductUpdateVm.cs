using System;

namespace EcommerceSolution.BackendAPI.ViewModels.Products
{
    public class ProductUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string UserUpdate { get; set; }
        public DateTime UpdateDate { get; set; }
        //public int Status { get; set; }
        //public string UserCreate { get; set; }
        //public DateTime CreateDate { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
