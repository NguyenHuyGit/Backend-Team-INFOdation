using EcommerceSolution.BackendAPI.ViewModels;
using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ESolutionDbContext _context;

        public ProductController(ESolutionDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAll()
        {
            List<Product> prod = await _context.Products.ToListAsync();
            if (prod != null)
            {
                return Ok(prod);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            Product prod = await _context.Products.SingleOrDefaultAsync(prod => prod.Id == id);
            if (prod != null)
            {
                return Ok(prod);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
