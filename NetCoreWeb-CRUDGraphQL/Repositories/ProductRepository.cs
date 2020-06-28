using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product item)
        {
            if (string.IsNullOrEmpty(item.Name)) await Task.FromException(new Exception("Product name is required"));
            _context.Products.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _context.Products.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            if (string.IsNullOrEmpty(item.Name)) await Task.FromException(new Exception("Product name is required"));
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
