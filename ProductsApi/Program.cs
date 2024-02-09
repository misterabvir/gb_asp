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
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "ProductsApi";
});

builder.Services.AddHttpClient();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton(provider => builder.Configuration.GetSection("ExternalLinks").Get<ExternalLinks>() ?? throw new Exception("ExternalLinks not found"));
builder.Services.AddScoped<IExternalQueryService, ExternalQueryService>();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();