using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL;
using CarvedRock.Api.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarvedRock.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarvedRockDbContext>(options =>
                options.UseSqlServer(_config["ConnectionStrings:CarvedRock"]));
            services.AddScoped<ProductRepository>();
            services.AddScoped<ProductReviewRepository>();

            // GraphQL
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(    // if something asks for IDependencyResolver return a new FuncDependencyResolver instance
                s.GetRequiredService));

            services.AddScoped<CarvedRockSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = true; })    // Register all the types GrahpQL.Net uses and additional options ( o.XY )
                .AddGraphTypes( ServiceLifetime.Scoped ) 
                .AddUserContextBuilder( httpContext => httpContext.User )             // User context(property) provider for authorization, whenever a user context is needed this will be executed
                .AddDataLoader();                                                     // For cashing data, no unnecessary queries
        }

        public void Configure(IApplicationBuilder app, CarvedRockDbContext dbContext)
        {
            app.UseGraphQL<CarvedRockSchema>();     // In argument we can specify the endpoint URI, /GraphQl default

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());   // To play around with the API without web application ( web app not yet created )
                                                                        // Sets up the default GraphQL playground at /ui/Playground
                                                                        // Project properties -> Debug -> Launch browser -> ui/Playground (starts the browser with the playground)

            // Fill the database
            dbContext.Seed();
        }
    }
}