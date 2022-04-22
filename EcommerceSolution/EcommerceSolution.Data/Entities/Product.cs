using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public Guid UserCreateId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserUpdateId { get; set; }
        public DateTime UpdateDate { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
    }
}
