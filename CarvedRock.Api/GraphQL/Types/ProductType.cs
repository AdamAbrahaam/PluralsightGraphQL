using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.Data.Entities;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL.Types
{
    // Query can't return entities or models directly
    public class ProductType : ObjectGraphType<Product>    // Meta data for product entity
    {
        public ProductType()
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("The name of the product."); // .Description() adds description for the field
            Field(t => t.Description);
        }
    }
}
