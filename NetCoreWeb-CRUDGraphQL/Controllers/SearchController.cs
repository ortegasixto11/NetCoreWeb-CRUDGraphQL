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
    public class SearchController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Search/ProductName
        [HttpGet("{productName}")]
        public async Task<ActionResult<IEnumerable<SearchDTO>>> Product(string productName)
        {

            List<SearchDTO> results = new List<SearchDTO>();
            var res = await _context.Products.Where(x => EF.Functions.Like(x.Name, $"%{productName}%"))
                .Include(x => x.StoreProducts).ThenInclude(z => z.Store)
                .ToListAsync();

            foreach (var item in res)
            {
                var result = new SearchDTO();
                result.ProductName = item.Name;
                result.Details = new List<SearchDetail>();
                foreach (var item2 in item.StoreProducts.OrderBy(x => x.Price))
                {
                    result.Details.Add(new SearchDetail { 
                        Price = item2.Price,
                        StoreName = item2.Store.Name,
                        StoreCity = item2.Store.City
                    });
                }
                results.Add(result);
            }
            return results;
        }

    }

    public class SearchDTO
    {
        public string ProductName { get; set; }
        public List<SearchDetail> Details { get; set; }
    }
    public class SearchDetail
    {
        public double Price { get; set; }
        public string StoreName { get; set; }
        public string StoreCity { get; set; }
    }

}