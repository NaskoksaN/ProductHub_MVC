using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductHub.DataAccess.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationService();

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
//show routing 
//app.Use(async (context, next) =>
//{
//    var endpoint = context.GetEndpoint();
//    Console.WriteLine("==--=============================================================================");
//    Console.WriteLine($"Request for {context.Request.Path} to endpoint: {endpoint?.DisplayName}");
//    Console.WriteLine("==--=============================================================================");
//    await next();
//});



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
