using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceSolution.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public List<Product> Products { get; set; }
    }
}
