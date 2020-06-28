using NetCoreWeb_CRUDGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Repositories
{
    public interface IProduct
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task DeleteByIdAsync(Guid id);
    }
}
