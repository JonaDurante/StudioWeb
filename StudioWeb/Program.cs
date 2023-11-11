using Microsoft.EntityFrameworkCore;
using StudioData.Data;
using Serilog;

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

builder.Services.AddDbContext<StudioWebContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<StudioWebUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<StudioWebContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseSerilogRequestLogging();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
