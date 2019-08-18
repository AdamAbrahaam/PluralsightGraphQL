using GraphQL.Types;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductTypeEnumType : EnumerationGraphType<Data.ProductType>   // Enum from data
    {
        public ProductTypeEnumType()
        {
            Name = "Type";
            Description = "The type of product";
        }
        
    }
}