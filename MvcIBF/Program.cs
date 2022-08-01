using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcIBF.Data;
using MvcIBF.Models;
using MvcIBF.Repository;
using MvcIBF.Repository.IRepository;
using AutoMapper;
using MvcIBF.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcIBFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcIBFContext") ?? throw new InvalidOperationException("Connection string 'MvcIBFContext' not found.")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new Helper());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
