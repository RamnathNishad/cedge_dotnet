using ADODemoMVC.Models;
using ADOLib;
using AutoMapper;
using WebAPIPrj.Models;

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

            builder.Services.AddSession(options =>
            {
                //options.IdleTimeout = new TimeSpan(0,0,10);
                
            });

            builder.Services.AddAuthentication("MyCookieAuth")
                .AddCookie("MyCookieAuth", options =>
                {
                    options.LoginPath = "/Account/Login";
                    //options.ExpireTimeSpan = new TimeSpan(0, 0, 10);
                    //options.SlidingExpiration = false;
                });

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<MySessionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSession();

            app.UseMiddleware<MySessionHandler>();
            app.Run();
        }
    }
}
