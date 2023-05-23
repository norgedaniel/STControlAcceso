using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_DataLib.Repositories;
using STCA_WebApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the STCA_DbContext to Services
builder.Services.AddDbContext<MSSQL_STCA_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddScoped<ISTCA_DbService, MSSQL_STCA_DbService>();

//builder.Services.AddScoped<IGenericRepository<ZonaHoraria>, GenericRepository<ZonaHoraria>>();

// register unit of work for work with data layer
builder.Services.AddScoped<ISCTA_UnitOfWork, SCTA_UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
