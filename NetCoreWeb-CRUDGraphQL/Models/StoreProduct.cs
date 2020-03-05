using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Models
{
    public class StoreProduct
    {
        public Guid ID { get; set; }
        public double Price { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Guid StoreID { get; set; }
        public Store Store { get; set; }
    }
}
