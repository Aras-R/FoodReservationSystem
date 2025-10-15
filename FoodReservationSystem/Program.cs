using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.UserFacade;
using FoodReservation.Application.Services.Foods.Facade;
using FoodReservation.Application.Services.Users.Facade;
using FoodReservation.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// ثبت Connection String به صورت مستقیم
var connectionString = @"Data Source=DESKTOP-8ILS0U2; Initial Catalog=FoodReservationDB; Integrated Security=True; TrustServerCertificate=True";

// ثبت DbContext و Interface
builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IUsersFacade, UsersFacade>();
builder.Services.AddScoped<IFoodsFacade, FoodsFacade>();

// فعال‌سازی MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// تنظیمات Middleware و Routing
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseRouting();
app.UseAuthorization();


app.MapStaticAssets();


// Area routes (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


// Default routes 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();



app.Run();
