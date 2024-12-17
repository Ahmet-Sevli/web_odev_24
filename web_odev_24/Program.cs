using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("Berber24Database");

// Add DbContext to the DI container
builder.Services.AddDbContext<web_odev_24.Models.BerberContext>(options =>
    options.UseSqlServer(connectionString));


// Add Authentication - Cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Hesap/Login";         // Giriþ sayfasý
        options.LogoutPath = "/Hesap/Logout";       // Çýkýþ sayfasý
        options.AccessDeniedPath = "/Hesap /AccessDenied"; // Yetkisiz eriþim sayfasý
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);  // Cookie süresi
        options.SlidingExpiration = true;             // Oturum süresini uzatma
    });

// Add Authorization (optional custom policies)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireClaim("Role", "Customer"));
});




















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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
