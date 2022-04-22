using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
    }
}
