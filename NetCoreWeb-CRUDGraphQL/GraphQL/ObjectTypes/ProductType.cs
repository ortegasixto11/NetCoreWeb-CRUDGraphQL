using GraphQL.Types;
using NetCoreWeb_CRUDGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWeb_CRUDGraphQL.GraphQL.ObjectTypes
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = "Product";
            Description = "Product fields";

            Field(x => x.ID).Description("Product ID");
            Field(x => x.Name).Description("Product Name");

        }
    }
}
