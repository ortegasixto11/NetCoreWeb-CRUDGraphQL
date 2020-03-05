using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;

namespace NetCoreWeb_CRUDGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoreProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StoreProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreProduct>>> GetStoreProducts()
        {
            return await _context.StoreProducts.ToListAsync();
        }

        // GET: api/StoreProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreProduct>> GetStoreProduct(Guid id)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);

            if (storeProduct == null)
            {
                return NotFound();
            }

            return storeProduct;
        }

        // PUT: api/StoreProducts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreProduct(Guid id, StoreProduct storeProduct)
        {
            if (id != storeProduct.ID)
            {
                return BadRequest();
            }

            _context.Entry(storeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StoreProducts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StoreProduct>> PostStoreProduct(StoreProduct storeProduct)
        {
            _context.StoreProducts.Add(storeProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreProduct", new { id = storeProduct.ID }, storeProduct);
        }

        // DELETE: api/StoreProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreProduct>> DeleteStoreProduct(Guid id)
        {
            var storeProduct = await _context.StoreProducts.FindAsync(id);
            if (storeProduct == null)
            {
                return NotFound();
            }

            _context.StoreProducts.Remove(storeProduct);
            await _context.SaveChangesAsync();

            return storeProduct;
        }

        private bool StoreProductExists(Guid id)
        {
            return _context.StoreProducts.Any(e => e.ID == id);
        }
    }
}
