
using ADOLib;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Xml.Serialization;
using WebAPIPrj.Models;

namespace WebAPIPrj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => {
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());               
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEmployeesRepository,EmployeeDataAccess>();
            
            //configure CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("clients-allowed", opt =>
                {
                    opt.WithOrigins("http://localhost:5055")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            //configure GlobalException Middleware
            builder.Services.AddScoped<GlobalExceptionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            //use CORS policy
            app.UseCors("clients-allowed");
            //use GlobalException Middleware
            app.UseMiddleware<GlobalExceptionHandler>();
            app.Run();
        }
    }
}
