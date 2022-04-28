using System;

namespace EcommerceSolution.BackendAPI.ViewModels.Products
{
    public class ProductDetails
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string brandName { get; set; }
        public string categoryName { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string userUpdate { get; set; }
        public DateTime? updateDate { get; set; }
        public int Status { get; set; }
    }
}
