using GivenAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddDbContext<PRN_Sum22_B1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"))); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "/api/[controller]/[action]");
app.Run();
