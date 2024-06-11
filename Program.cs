
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Futuristic.Data;
using Futuristic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var webApplicationBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");
webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>();
webApplicationBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();

webApplicationBuilder.Services
    .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

webApplicationBuilder.Services.AddScoped<UserManager<ApplicationUser>>();

webApplicationBuilder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

webApplicationBuilder.Services.AddControllersWithViews();

webApplicationBuilder.Services.AddRazorPages();

var webApplication = webApplicationBuilder.Build();

var logger = webApplication.Services.GetService<ILogger<Program>>()!;

// Configure the HTTP request pipeline.
if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseDeveloperExceptionPage();

    webApplication.UseMigrationsEndPoint();
}
else
{
    webApplication.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    webApplication.UseHsts();
}

webApplication.UseHttpsRedirection();

var supportedCultures = new[]
{
   new CultureInfo("en-US")
};

webApplication.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

webApplication.UseStaticFiles();

webApplication.UseRouting();

webApplication.UseAuthentication();
webApplication.UseAuthorization();

webApplication.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
webApplication.MapRazorPages();

webApplication.ApplyDatabaseSeeding(logger);

webApplication.Run();
