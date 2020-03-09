using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreWeb_CRUDGraphQL.Data;
using NetCoreWeb_CRUDGraphQL.DTO;
using NetCoreWeb_CRUDGraphQL.Models;

namespace NetCoreWeb_CRUDGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Search/ProductName
        [HttpGet("{productNameSearch}")]
        public async Task<ActionResult<IEnumerable<SearchResult>>> Product(string productNameSearch)
        {

            List<SearchResult> results = new List<SearchResult>();
            List<Product> resultSearch = new List<Product>();

            if (string.IsNullOrEmpty(productNameSearch))
            {
                resultSearch = await _context.Products
                .Include(x => x.StoreProducts).ThenInclude(z => z.Store)
                .ToListAsync();
            }
            else
            {
                resultSearch = await _context.Products.Where(x => EF.Functions.Like(x.Name, $"%{productNameSearch}%"))
                .Include(x => x.StoreProducts).ThenInclude(z => z.Store)
                .ToListAsync();
            }

            foreach (var item in resultSearch)
            {
                foreach (var item2 in item.StoreProducts)
                {
                    results.Add(new SearchResult
                    { 
                        ProductName = item.Name,
                        Price = item2.Price,
                        StoreName = item2.Store.Name,
                        StoreCity = item2.Store.City
                    });
                }
            }

            return results.OrderBy(x => x.Price).ToList();
        }

    }
}