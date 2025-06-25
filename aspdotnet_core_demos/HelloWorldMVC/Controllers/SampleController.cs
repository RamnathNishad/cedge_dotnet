using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Serialization;

namespace HelloWorldMVC.Controllers
{
    public class SampleController : Controller
    {
        private readonly IDemo demo1,demo2;
        private readonly IMyFileLogger logger;
        public SampleController(IDemo demo1,IDemo demo2, IMyFileLogger logger)
        {
            this.demo1 = demo1;
            this.demo2 = demo2;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                int a = 100, b = 0;
                int result = a / b;

                var y = this.HttpContext.Session.GetInt32("y");
                ViewData.Add("y", y);
                return View();
            }
            catch (DivideByZeroException ex)
            {
                logger.Log(ex.Message);

                ViewData.Add("errMsg", ex.Message);
                return View("DisplayErrors");
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                ViewData.Add("errMsg", ex.Message);
                return View("DisplayErrors");
            }
        }

        public IActionResult Display()
        {     
            demo1.Increment();
            demo2.Increment();
            ViewData.Add("c1", demo1.counter);
            ViewData.Add("c2", demo2.counter);
            return View();
        }
        public IActionResult Show()
        {
            demo1.Increment();
            demo2.Increment();
            ViewData.Add("c1", demo1.counter);
            ViewData.Add("c2", demo2.counter);
            return View();            
        }
    }    
}
