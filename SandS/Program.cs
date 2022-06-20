using Microsoft.EntityFrameworkCore;
//using AspNetCoreHero.ToastNotification;
using Service;
using Service.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Microsoft.AspNetCore.Mvc.Razor;
using Service.Repository.IRepository;
using Service.Repository;
using Service.Dbinitializer;
using Service.DbInitializer;
using NToastNotify;
using S_and_S.Hubs;
using S_and_S.MiddlewareExtensions;
using S_and_S.SubscribeTableDependencies;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Environment.EnvironmentName = Environments.Production;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Identity/Account/Login";
//    options.LogoutPath = "/Identity/Account/Logout";
//    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddAntiforgery(options =>
//{
//    options.Cookie.Name = "X-CSRF-TOKEN-MOONGLADE";
//    options.FormFieldName = "CSRF-TOKEN-MOONGLADE-FORM";
//});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//Add users without roles
//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<Model.ApplicationUser,IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddSingleton<IEmailSender,EmailSender>();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//builder.Services.AddTransient<DataHandler, DataHandler>();
//builder.Services.AddSingleton<Utils, Utils>();

builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
//NB: Nkosana
//builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddScoped<DashboardHub>();
builder.Services.AddScoped<SubscribePendingLaundryDependency>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;
//});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

//    options.LoginPath = "/Identity/Account/Login";
//    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//    options.SlidingExpiration = true;
//});

builder.Services.AddMemoryCache();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Added from SOF
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseNToastNotify();
app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
//SeedDatabase();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapHub<DashboardHub>("/dashboardHub");

app.UseSession();

app.MapRazorPages(); //NB :  needed  for Razor pages for routing i.e Identity 

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
//     pattern: "{controller=Admin}/{action=Index}/{id?}");
pattern: "{controller=Home}/{action=Index}/{id?}");
//pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}");

var contentRoot = app.Environment.WebRootPath;

Rotativa.AspNetCore.RotativaConfiguration.Setup(contentRoot, "rotativa");


//app.UsePendingLaundry();

app.Run();

void SeedDatabase() 
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitilizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitilizer.Initialize();
    }
}
