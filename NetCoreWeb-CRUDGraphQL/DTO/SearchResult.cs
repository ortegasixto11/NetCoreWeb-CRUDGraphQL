using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.DTO
{
    public class SearchResult
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string StoreName { get; set; }
        public string StoreCity { get; set; }
    }
}
