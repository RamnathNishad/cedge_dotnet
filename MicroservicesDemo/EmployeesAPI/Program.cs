
using Consul;
using EmployeesAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IEmployeDataAccess, EmployeeDataAcccess>();


            var secretKey = builder.Configuration["JWT:Key"];

            builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer",options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            //Register EmpService with Consul 
            var consulClient = new ConsulClient(c =>
            {
                c.Address = new Uri(builder.Configuration["ConsulConfig:Address"]);
            });

            var registration = new AgentServiceRegistration()
            {
                ID = builder.Configuration["ConsulConfig:ServiceId"],
                Name = builder.Configuration["ConsulConfig:ServiceName"],
                Address = builder.Configuration["ConsulConfig:ServiceHost"],
                Port = int.Parse(builder.Configuration["ConsulConfig:ServicePort"]),
                //Check = new AgentServiceCheck()
                //{
                //    HTTP = $"http://{builder.Configuration["ConsulConfig:ServiceHost"]}:{builder.Configuration["ConsulConfig:ServicePort"]}/health",
                //    Interval=TimeSpan.FromSeconds(10),
                //    Timeout=TimeSpan.FromSeconds(5),
                //    DeregisterCriticalServiceAfter=TimeSpan.FromMinutes(1)
                //}
            };

            consulClient.Agent.ServiceRegister(registration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
