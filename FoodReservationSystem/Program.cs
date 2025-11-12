using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.Reservation;
using FoodReservation.Application.Interfaces.FacadePatterns.UserFacade;
using FoodReservation.Application.Services.DailyFoods.Facade;
using FoodReservation.Application.Services.Foods.Facade;
using FoodReservation.Application.Services.Reservations.Facade;
using FoodReservation.Application.Services.Users.Facade;
using FoodReservation.Persistence.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 💾 تنظیم Connection String
var connectionString = @"Data Source=DESKTOP-8ILS0U2; Initial Catalog=FoodReservationDB; Integrated Security=True; TrustServerCertificate=True";

// 📦 ثبت DbContext و سرویس‌ها
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();
builder.Services.AddScoped<IUsersFacade, UsersFacade>();
builder.Services.AddScoped<IFoodsFacade, FoodsFacade>();
builder.Services.AddScoped<IDailyFoodsFacade, DailyFoodFacade>();
builder.Services.AddScoped<IReservationsFacade, ReservationsFacade>();

// 🔐 فعال‌سازی احراز هویت با کوکی
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/SignIn"; // مسیر لاگین
        options.LogoutPath = "/Authentication/SignUp"; // مسیر لاگ‌اوت
        options.AccessDeniedPath = "/Authentication/AccessDenied"; // در صورت عدم دسترسی
        options.ExpireTimeSpan = TimeSpan.FromHours(4); // مدت زمان ماندگاری کوکی
        options.SlidingExpiration = true; // تمدید خودکار در هر درخواست
    });

// ⚙️ فعال‌سازی MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔧 Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ⚠️ مهم: ترتیب Authentication قبل از Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// 🔹 مسیرهای Area (مثل Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// 🔹 مسیر پیش‌فرض
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();

app.Run();
