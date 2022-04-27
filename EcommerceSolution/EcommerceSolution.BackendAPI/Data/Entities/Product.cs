using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.BackendAPI.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
