using HelloWorldMVC.Controllers;
using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace HelloWorldMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //configure session
            builder.Services.AddSession();

            //configure dependency injection for Calculator instance
            builder.Services.AddSingleton<ICalculator,Calculator>();

            builder.Services.AddSingleton<IDemo,Demo>();

            builder.Services.AddTransient<IMyFileLogger, FileLogger>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");                
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Welcome}/{id?}");

            app.UseSession();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseMiddleware<GlobalExceptionHandler>();

            app.Run();
        }
    }
}
