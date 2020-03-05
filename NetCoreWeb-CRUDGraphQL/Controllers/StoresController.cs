using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using NetCoreWeb_CRUDGraphQL.Repositories;

namespace NetCoreWeb_CRUDGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly StoreRepository _repo;

        public StoresController(ApplicationDbContext context)
        {
            _repo = new StoreRepository(context);
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            return await _repo.GetAllAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(Guid id)
        {
            var store = await _repo.GetByIdAsync(id);
            if (store == null) return NotFound("Error Not Found");
            return store;
        }

        // PUT: api/Stores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(Guid id, Store store)
        {
            if (id != store.ID) return BadRequest("Error");

            try
            {
                await _repo.UpdateAsync(store);
                return Ok("Update Ok");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // POST: api/Stores
        [HttpPost]
        public async Task<IActionResult> PostStore(Store store)
        {
            try
            {
                await _repo.CreateAsync(store);
                return Ok($"Create Ok => {store.ID}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var store = await _repo.GetByIdAsync(id);
            if (store == null) return NotFound("Error Not Found");
            await _repo.DeleteAsync(store);
            return Ok("Delete Ok");
        }

    }
}
