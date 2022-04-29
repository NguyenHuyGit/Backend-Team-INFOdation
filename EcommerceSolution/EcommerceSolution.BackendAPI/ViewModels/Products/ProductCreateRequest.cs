using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.ViewModels.Products
{
    public class ProductCreateRequest
    {
       
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }


    }
}
