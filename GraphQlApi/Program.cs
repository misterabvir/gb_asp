using GraphQlApi.Categories;
using GraphQlApi.GraphQl;
using GraphQlApi.Services;
using GraphQlApi.Stocks;
using GraphQlApi.Stores;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddMutationType(q => q.Name("Mutation"))
    .AddType<QueryProducts>()
    .AddType<QueryCategories>()
    .AddType<QueryStores>()
    .AddType<QueryStocks>()
    .AddType<MutationProducts>()
    .AddType<MutationStores>()
    .AddType<MutationStocks>()
    .AddType<MutationCategories>();
;

var app = builder.Build();

app.MapGraphQL();


app.Run();
