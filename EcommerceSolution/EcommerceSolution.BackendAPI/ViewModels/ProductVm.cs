using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.ViewModels.Product
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int? CategoryId { get; set; }
    }
}