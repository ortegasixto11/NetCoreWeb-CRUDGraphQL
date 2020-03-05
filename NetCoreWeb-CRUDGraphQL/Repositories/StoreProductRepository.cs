using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Repositories
{
    public class StoreProductRepository : IBaseRepository<StoreProduct>
    {
        private readonly ApplicationDbContext _context;

        public StoreProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(StoreProduct item)
        {
            if (item.Price < 1) await Task.FromException(new Exception("Price is required"));
            else if (item.ProductID == null || item.ProductID == Guid.Empty) await Task.FromException(new Exception("Product is required"));
            else if (item.StoreID == null || item.StoreID == Guid.Empty) await Task.FromException(new Exception("Store is required"));

            _context.StoreProducts.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(StoreProduct item)
        {
            _context.StoreProducts.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _context.StoreProducts.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<StoreProduct>> GetAllAsync()
        {
            return await _context.StoreProducts
                .Include(x => x.Store)
                .Include(x => x.Product)
                .Select(x => new StoreProduct { 
                    ID = x.ID,
                    Price = x.Price,
                    ProductName = x.Product.Name,
                    ProductID = x.ProductID,
                    StoreName = x.Store.Name,
                    StoreCity = x.Store.City,
                    StoreID = x.StoreID
                })
                .ToListAsync();
        }

        public async Task<StoreProduct> GetByIdAsync(Guid id)
        {
            return await _context.StoreProducts
                .Where(x => x.ID == id)
                .Include(x => x.Store)
                .Include(x => x.Product)
                .Select(x => new StoreProduct
                {
                    ID = x.ID,
                    Price = x.Price,
                    ProductName = x.Product.Name,
                    ProductID = x.ProductID,
                    StoreName = x.Store.Name,
                    StoreCity = x.Store.City,
                    StoreID = x.StoreID
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(StoreProduct item)
        {
            var storeProduct = await GetByIdAsync(item.ID);
            if (storeProduct == null) await Task.FromException(new Exception("Error Product Not Found"));
            storeProduct.Price = item.Price;

            _context.StoreProducts.Update(storeProduct);
            await _context.SaveChangesAsync();
        }
    }
}
