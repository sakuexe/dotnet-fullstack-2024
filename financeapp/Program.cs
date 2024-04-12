using Microsoft.EntityFrameworkCore;
using financeapp.Data;
using financeapp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FinancesContext") ??
    throw new InvalidOperationException("Connection string 'FinancesContext' not found.");

builder.Services.AddDbContext<FinancesContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.Name = "FinanceLoginCookie";
        options.AccessDeniedPath = "/Login/Logout";
        options.LogoutPath = "/";
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("NoAdminAccess", policy =>
    {
        policy.RequireAssertion(context =>
            !(context.User.Identity!.IsAuthenticated && context.User.IsInRole("Admin")));
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // seed data to the database
    // it sets the admin user if it doesn't exist
    var services = scope.ServiceProvider;
    SeedData.Initialize(services, app.Configuration);
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

app.UseAuthentication(); // Authentication middleware must be added before Authorization middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
