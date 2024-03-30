using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagementSystem.Models;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Services.AddLogging(logging =>
{
    logging.AddConfiguration(config.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();
    logging.AddNLog();
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<TMSDbContext>();
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddDbContextPool<TMSDbContext>(options =>
options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
