using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Models
{
    public class Store
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
