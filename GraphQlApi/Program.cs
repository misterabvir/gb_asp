using ExternalLinks;
using ExternalLinks.Base;
using GraphQlApi.Categories;
using GraphQlApi.GraphQl;
using GraphQlApi.Stocks;
using GraphQlApi.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<QueryProducts>()
    .AddType<QueryCategories>()
    .AddType<QueryStores>()
    .AddType<QueryStocks>()
    .AddMutationType(q => q.Name("Mutation"))
    .AddType<MutationProducts>()
    .AddType<MutationStores>()
    .AddType<MutationStocks>()
    .AddType<MutationCategories>();


var app = builder.Build();
app.MapGraphQL();


app.Run();
