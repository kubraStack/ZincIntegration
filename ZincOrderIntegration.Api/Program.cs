using BusinessLayer.Service;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
         builder =>
         {
             builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
         });
});
// Add services to the container.
var userMockService = true; // API key gelince false olacak
if (userMockService)
{
    builder.Services.AddScoped<IZincOrderService, ZincOrderServiceMock>();
}
else
{
    //böylelikle fake ve real servislerin arasýna true false yaparak geçiþ yapýlabilir.
    builder.Services.AddScoped<IZincOrderService, ZincOrderServiceReal>();
    builder.Services.AddHttpClient<ZincOrderServiceReal>(); //HttpClient injection
}
    builder.Services.AddScoped<IZincOrderService, ZincOrderServiceMock>();
builder.Services.AddDbContext<ZincDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZincDbConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
