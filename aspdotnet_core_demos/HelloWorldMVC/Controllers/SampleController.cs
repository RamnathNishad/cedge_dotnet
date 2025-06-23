using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers
{
    public class SampleController : Controller
    {

        private readonly IDemo demo;

        public SampleController(IDemo demo)
        {
            this.demo = demo;
        }

        public IActionResult Index()
        {
            var y = this.HttpContext.Session.GetInt32("y");
            ViewData.Add("y", y);
            return View();
        }


        public IActionResult Display()
        {
            demo.Increment();
            ViewData.Add("Counter", demo.id);
            return View();
            //return RedirectToAction("Show");
        }
        public IActionResult Show()
        {
            demo.Increment();
            ViewData.Add("Counter", demo.id);
            return View("Show");
        }

    }
}
