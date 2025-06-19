using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            //store data in Cookies
            HttpContext.Response.Cookies.Append("accno", "1234567");
            HttpContext.Response.Cookies.Append("cname", "Ramnath");

            return View();
        }

        public IActionResult Display()
        {
            //access data from Cookies
            var accno = HttpContext.Request.Cookies["accno"];
            var cname = HttpContext.Request.Cookies["cname"];

            ViewBag.accno=accno;
            ViewBag.cname=cname;

            return View();
        }

        public IActionResult FirstPage()
        {
            return View();
        }

        public IActionResult SecondPage(int a,int b)
        {
            ViewData.Add("a", a);
            ViewData.Add("b", b);

            return View();
        }
    }
}
