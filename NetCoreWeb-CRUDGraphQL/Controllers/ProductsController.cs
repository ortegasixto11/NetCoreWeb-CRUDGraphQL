using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using NetCoreWeb_CRUDGraphQL.Repositories;

namespace NetCoreWeb_CRUDGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repo;

        public ProductsController(ApplicationDbContext context)
        {
            _repo = new ProductRepository(context);
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _repo.GetAllAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound("Error Not Found");
            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.ID) return BadRequest("Error");

            try
            {
                await _repo.UpdateAsync(product);
                return Ok("Update Ok");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct(Product product)
        {
            try
            {
                await _repo.CreateAsync(product);
                return Ok($"Create Ok => {product.ID}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound("Error Not Found");
            await _repo.DeleteAsync(product);
            return Ok("Delete Ok");
        }
    }
}
