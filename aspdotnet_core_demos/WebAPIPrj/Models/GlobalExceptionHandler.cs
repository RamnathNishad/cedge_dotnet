
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebAPIPrj.Models
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               await next(context);
            }
            catch (Exception ex)
            {
                //send the error response with proper details as per user
                ProblemDetails details = new ProblemDetails
                {
                    Detail="Some server error occurred:"+ ex.Message,
                    Status=(int)HttpStatusCode.InternalServerError,
                    Title="Server error",
                    Type="Internal Server error"
                };

                //serialize this details into json
                string json=JsonSerializer.Serialize(details);
                //set the content type of response as json
                context.Response.ContentType = "application/json";
                //send the error response
                await context.Response.WriteAsync(json);
            }
        }
    }
}
