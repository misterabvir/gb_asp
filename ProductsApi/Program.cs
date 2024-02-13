using ExternalLinks;
using TokenManager;
using ExternalLinks.Base;
using Microsoft.EntityFrameworkCore;
using ProductsApi.BusinessLogicalLayer.Services;
using ProductsApi.BusinessLogicalLayer.Services.Base;
using ProductsApi.DataAccessLayer.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Db
builder.Services.AddDbContext<ProductsContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBaseConnection"));
});

//cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "ProductsApi";
});



builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();


builder.Services.AddTokenManager(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
