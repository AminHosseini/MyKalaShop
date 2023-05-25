using _0_Framework.Application;
using DiscountManagement.Infrastructure.Configuration;
using ServiceHost;
using InventoryManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using AccountManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopManagement.Infrastructure.Configuration;
using _0_Framework.Application.Sms;
using ShopManagement.Presentation.Api;
using InventoryManagement.Presentation.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddMvcOptions(options => options.Filters.Add<SecurityControllerFilter>())
    .AddApplicationPart(typeof(ProductApiController).Assembly)
    .AddApplicationPart(typeof(InventoryApiController).Assembly)
    .AddNewtonsoftJson();

builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("MyKalaShop");

ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<ISmsService, SmsService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IAuthHelper, AuthHelper>();
//builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services
    .Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.Lax;
    });

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
    {
        x.LoginPath = new PathString("/Account/Index");
        x.AccessDeniedPath = new PathString("/Home/AccessDenied");
    });

builder.Services.AddAuthorization();

//builder.Host.ConfigureLogging((context, logging) =>
//{
//    logging.ClearProviders();
//    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
//    logging.AddDebug();
//    logging.AddConsole();
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
