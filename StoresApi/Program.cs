using TokenManager;
using ExternalLinks;
using ExternalLinks.Base;
using Microsoft.EntityFrameworkCore;
using StoresApi.BusinessLogicalLayer.Services;
using StoresApi.BusinessLogicalLayer.Services.Base;
using StoresApi.DataAccessLayer.Contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<StoresContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBaseConnection"));
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "StoresApi";
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IStoreService, StoreService>();


builder.Services.AddTokenManager(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();



app.MapControllers();

app.Run();