using AmazonShoping.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

//generate log file for each execution of the program with a date in the name
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File($@"{Directory.GetCurrentDirectory()}\Logs\log-{DateTime.Now:yyyy-MM-dd_hh-mm-ss-tt}.txt")
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SoukMVVMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonCLoneContextSQLServer")));

builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Sook.ma.Session";
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.MaxAge = TimeSpan.FromDays(7);
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();