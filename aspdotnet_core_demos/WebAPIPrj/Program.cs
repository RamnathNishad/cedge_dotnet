
using ADOLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

            var secretKey = builder.Configuration["JWT:Key"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //use authentication and in this sequence only
            app.UseAuthentication();
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
