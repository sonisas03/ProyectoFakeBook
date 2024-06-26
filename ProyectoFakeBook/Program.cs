using Microsoft.EntityFrameworkCore;
using ProyectoFakeBook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FakeBookDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));
builder.Services.AddControllers();

var app = builder.Build();

// Configure CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
