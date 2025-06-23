using HelloWorldMVC.Models;

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

            builder.Services.AddScoped<IDemo,Demo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Welcome}/{id?}");

            app.UseSession();

            
            app.Run();
        }
    }
}
