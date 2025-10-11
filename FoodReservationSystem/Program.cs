using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// ??? Connection String ?? ???? ??????
var connectionString = "@\"Data Source=DESKTOP-8ILS0U2; Initial Catalog=FoodReservationDB; Integrated Security=True; TrustServerCertificate=True;";

// ??? DbContext ? Interface
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IDatabaseContext, DatabaseContext>();

// ????????? MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ??????? Middleware ? Routing
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
).WithStaticAssets();


app.Run();
