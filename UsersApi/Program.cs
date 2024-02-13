using TokenManager;
using UsersApi.BusinessLogicalLayer;
using UsersApi.DataAccessLayer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTokenManager(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicalLayer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
