using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Repositories
{
    public class StoreRepository : IBaseRepository<Store>
    {
        private readonly ApplicationDbContext _context;


        public StoreRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DB_CRUD_GraphQL;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Store item)
        {
            _context.Stores.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Store item)
        {
            _context.Stores.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var store = await GetByIdAsync(id);
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Store>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> GetByIdAsync(Guid id)
        {
            return await _context.Stores.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Store item)
        {
            _context.Stores.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
