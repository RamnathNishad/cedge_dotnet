using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HelloWorldMVC.Models;

namespace HelloWorldMVC.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            ViewData.Add("message", "Hello from MVC");
            ViewBag.Heading = "Working with ASP.NET Core MVC";
            TempData.Add("x", 100);
            this.HttpContext.Session.SetInt32("y", 200);

            //model
            var emp = new Employee
            {
                Ecode=101,
                Ename="Ravi",
                Salary=1111,
                Deptid=201
            };

            return View(emp);
        }

        public IActionResult Welcome()
        {
            //model
            var lstEmps = new List<Employee>
            {
                new Employee{Ecode=101,Ename="Ravi",Salary=1111,Deptid=201},
                new Employee{Ecode=102,Ename="Rahul",Salary=2222,Deptid=202},
                new Employee{Ecode=103,Ename="Rohit",Salary=3333,Deptid=203}
            };
            return View(lstEmps);
        }
    }
}
