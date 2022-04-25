using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.BackendAPI.Data.Entities
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int BrandId { get; set; }
        public List<Product> Products { get; set; }
        public Brand Brand { get; set; }
    }
}
