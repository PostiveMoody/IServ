using IServ.DAL;
using IServ.WebApplication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ServDbContext>(options =>
    options.UseSqlServer(conn));

builder.Services.AddTransient<IConventionModelFactory, EdmModelFactory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
