using System;

namespace EcommerceSolution.BackendAPI.ViewModels.Products
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
