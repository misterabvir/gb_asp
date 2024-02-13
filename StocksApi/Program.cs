using ExternalLinks;
using TokenManager;
using ExternalLinks.Base;
using Microsoft.EntityFrameworkCore;
using StocksApi.BusinessLogicalLayer.Services;
using StocksApi.BusinessLogicalLayer.Services.Base;
using StocksApi.DataAccessLayer.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Db
builder.Services.AddDbContext<StocksContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBaseConnection"));
});

//cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "StocksApi";
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IStockService, StockService>();


builder.Services.AddTokenManager(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
