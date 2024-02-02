using Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});


builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//    options.RoutePrefix = string.Empty;
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
