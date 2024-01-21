using SEM3.Abstractions;
using SEM3.Mapper;
using SEM3.Models;
using SEM3.Mutations;
using SEM3.Query;
using SEM3.Services;

namespace SEM3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof (MapperProfile));
            builder.Services.AddSingleton<IProductServices, ProductServices>();
            builder.Services.AddSingleton<IStorageServices, StoreServices>();
            builder.Services.AddSingleton<IGroupServices, GroupServices>();


            builder.Services
                .AddGraphQLServer()
                .AddQueryType<MyQuery>();
            builder.Services
                   .AddGraphQLServer()
                   .AddMutationType<MyMutation>();
            builder.Services.AddSingleton<StoreContext>();

            var app = builder.Build();

            app.MapGraphQL();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
