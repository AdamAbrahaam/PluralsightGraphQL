using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository productRepository)
        {
            Field<ListGraphType<ProductType>>(      // I want to return a list => ListGraphType
                "products",
                resolve: context => productRepository.GetAll()
                );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>  // NOT optional ID argument
                    {Name = "id"}),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");    // Get the specific ID from context
                    return productRepository.GetOne(id);
                });
        }
    }
}
