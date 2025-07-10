using FluentValidation;
using FluentValidation.AspNetCore;
using KentBlog.Business;
using KentBlog.Data;
using KentBlog.Data.Concrete;
using KentBlog.Data.Seed;
using KentBlog.Entity.Concrete;
using KentBlog.UI.ValidationRules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// FluentValidation ayarlarý
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserViewModelValidator).Assembly);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "MyAppAuthCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;

    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await IdentityDataInitializer.SeedRoles(roleManager);
    await IdentityDataInitializer.SeedUsers(userManager, roleManager);


    var context = scope.ServiceProvider.GetRequiredService<Context>();
    await IdentityDataInitializer.SeedAdminSettings(context);
    await IdentityDataInitializer.SeedThemeSettings(context);
    await IdentityDataInitializer.SeedGeneralSettings(context);
    await IdentityDataInitializer.SeedMainPages(context);
    await IdentityDataInitializer.SeedMenu(context);
}

// Configure the HTTP request pipeline.
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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();