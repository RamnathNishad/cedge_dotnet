using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers
{
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            var y = this.HttpContext.Session.GetInt32("y");
            ViewData.Add("y", y);
            return View();
        }
    }
}
