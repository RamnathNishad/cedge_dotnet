
namespace WebAPIPrj.Models
{
    public class MySessionHandler : IMiddleware
    {
        int? startMinute = 0,endMinute=0;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            startMinute = DateTime.Now.Minute;
            endMinute = DateTime.Now.Minute;
            if (context.Session.GetInt32("startTime") == null)
            {
                context.Session.SetInt32("startTime", (int)startMinute);
            }
            else
            {
                startMinute = context.Session.GetInt32("startTime");
            }

            if ((endMinute - startMinute) > 1)
            {
                //perform some session clearance logic
                Console.WriteLine("session expired after 1 minutes");
                context.Session.Clear();
                context.Response.Redirect("/Account/Login");
            }
            await next(context);
        }
    }
}
