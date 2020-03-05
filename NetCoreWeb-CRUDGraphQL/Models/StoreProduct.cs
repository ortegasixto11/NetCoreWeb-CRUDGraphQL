using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.Models
{
    public class StoreProduct
    {
        public Guid ID { get; set; }
        public double Price { get; set; }
        public Guid ProductID { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public Guid StoreID { get; set; }
        //[JsonIgnore]
        public Store Store { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public string StoreName { get; set; }
        [NotMapped]
        public string StoreCity { get; set; }
    }
}
