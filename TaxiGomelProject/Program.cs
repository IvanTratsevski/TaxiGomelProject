using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TaxiGomelProject.Data;
using TaxiGomelProject.Services;
using TaxiGomelProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TaxiGomelProject.Middleware;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string conStr = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<TaxiGomelContext>(options => options.UseSqlServer(conStr));
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICachedService<Car>, CachedCarsService>();
builder.Services.AddScoped<ICachedService<CarModel>, CachedCarModelsService>();
builder.Services.AddScoped<ICachedService<Rate>, CachedRatesService>();
builder.Services.AddScoped<ICachedService<Employee>, CachedEmployeesService>();
builder.Services.AddScoped<ICachedService<CarDriver>, CachedCarDriversService>();
builder.Services.AddScoped<ICachedService<CarMechanic>, CachedCarMechanicsService>();
builder.Services.AddScoped<ICachedService<Position>, CachedPositionsService>();
builder.Services.AddScoped<ICachedService<Call>, CachedCallsService>();
var usersConnectionString = builder.Configuration.GetConnectionString("UsersConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(usersConnectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();



var app = builder.Build();
app.UseDbInitializer();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
