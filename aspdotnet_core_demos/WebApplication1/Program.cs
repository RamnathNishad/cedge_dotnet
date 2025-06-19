namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            //use the static content fetching middleware
            app.UseStaticFiles();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
