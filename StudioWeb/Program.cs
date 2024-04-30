using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudioData.Data;
using StudioData.Interfaces;
using StudioData.Services;
using StudioWeb;

var builder = WebApplication.CreateBuilder(args);
////Config Serilog
builder.Host.UseSerilog((HostBuilderCtx, LoggerConf) =>
{
    LoggerConf
    .WriteTo.Console() // Escribe en la consola
    .WriteTo.Debug()   // Escriba en debug
    .ReadFrom.Configuration(HostBuilderCtx.Configuration);
});
var connectionString = builder.Configuration.GetConnectionString("StudioWebContextConnection") ?? throw new InvalidOperationException("Connection string 'StudioWebContextConnection' not found.");

builder.Services
    .AddDbContext<StudioWebContext>(options => options.UseSqlServer(connectionString))
    .AddDefaultIdentity<StudioWebUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        })
    .AddEntityFrameworkStores<StudioWebContext>()
    .AddDefaultTokenProviders();

builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ICourseServices, CourseServices>();
builder.Services.AddScoped(typeof(ICommonServices<>), typeof(CommonServices<>));
builder.Services.AddScoped<IThirdServices, ThirdServices>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles()
   .UseHttpsRedirection()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization()
   .UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
