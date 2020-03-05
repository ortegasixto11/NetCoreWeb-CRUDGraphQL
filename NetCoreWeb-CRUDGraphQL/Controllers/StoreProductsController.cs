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
    public class StoreProductsController : ControllerBase
    {
        private readonly StoreProductRepository _repo;

        public StoreProductsController(ApplicationDbContext context)
        {
            _repo = new StoreProductRepository(context);
        }

        // GET: api/StoreProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProduct>>> GetStoreProducts()
        {
            return await _repo.GetAllAsync();
        }

        // GET: api/StoreProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProduct>> GetStoreProduct(Guid id)
        {
            var storeProduct = await _repo.GetByIdAsync(id);
            if (storeProduct == null) return NotFound("Error Not Found");
            return storeProduct;
        }

        // PUT: api/StoreProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreProduct(Guid id, StoreProduct storeProduct)
        {
            if (id != storeProduct.ID) return BadRequest("Error");

            try
            {
                await _repo.UpdateAsync(storeProduct);
                return Ok("Update Ok");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // POST: api/StoreProducts
        [HttpPost]
        public async Task<ActionResult<StoreProduct>> PostStoreProduct(StoreProduct storeProduct)
        {
            try
            {
                await _repo.CreateAsync(storeProduct);
                return Ok($"Create Ok => {storeProduct.ID}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // DELETE: api/StoreProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreProduct>> DeleteStoreProduct(Guid id)
        {
            var storeProduct = await _repo.GetByIdAsync(id);
            if (storeProduct == null) return NotFound("Error Not Found");
            await _repo.DeleteAsync(storeProduct);
            return Ok("Delete Ok");
        }

    }
}
