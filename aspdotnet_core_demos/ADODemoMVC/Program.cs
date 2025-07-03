using ADODemoMVC.Models;
using ADOLib;
using AutoMapper;

namespace ADODemoMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //configure dependency injection
            //builder.Services.AddScoped<IEmployeesRepository, EmployeeDataAccess>();
            //builder.Services.AddScoped<IEmployeesRepository, AdoDisconnected>();
            builder.Services.AddScoped<IEmployeesRepository,AdoDisconnectedOleDb>();

            //configure AutoMapper
            MapperConfiguration config = new MapperConfiguration(c => c.AddProfile(new EmployeeProfile()));
            IMapper mapper = config.CreateMapper();
            //add this mapper to the services
            builder.Services.AddSingleton(mapper);

            builder.Services.AddSession();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSession();
            app.Run();
        }
    }
}
