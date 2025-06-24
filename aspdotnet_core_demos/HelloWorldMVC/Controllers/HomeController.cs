using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelloWorldMVC.Controllers
{
    public class HomeController : Controller
    {
        //dependency inject using constructor
        private readonly ICalculator calculator;

        private readonly IDemo demo1,demo2;
        public HomeController(ICalculator calculator, IDemo demo1,IDemo demo2)
        {
            this.calculator = calculator;
            this.demo1 = demo1;   
            this.demo2 = demo2;
        }

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

        public IActionResult Demo()
        {
            return View("View");
        }
        public IActionResult Welcome()
        {
            //model
            var lstEmps = Employee.Employees;
            return View(lstEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new EmployeeVM
            {
                DeptIds =new  List<SelectListItem>
                {
                    new SelectListItem { Text = "Account", Value = "201" },
                    new SelectListItem { Text = "Admin", Value = "202" },
                    new SelectListItem { Text = "Sales", Value = "203" }
                } 
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM employee)
        {
            //server-side validation for dupilicate checking
            if (IsECodeExists(employee.Ecode))
            {
                //duplicate error
                ModelState.AddModelError("Ecode", "ecode already exists");
            }


            if (ModelState.IsValid)
            {
                var record = new Employee
                {
                    Ecode= employee.Ecode,
                    Ename = employee.Ename,
                    Salary = employee.Salary,
                    Deptid = employee.Deptid
                };
                Employee.Employees.Add(record);
                return RedirectToAction("Welcome");
            }
            else
            {
                employee.DeptIds = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Account", Value = "201" },
                    new SelectListItem { Text = "Admin", Value = "202" },
                    new SelectListItem { Text = "Sales", Value = "203" }
                };
                return View(employee);
            }           
        }

        private bool IsECodeExists(int ecode)
        {
            foreach (var item in Employee.Employees)
            {
                if(item.Ecode == ecode) return true;
            }
            return false;
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            //delete the record by id
            var emp = Employee.Employees.Find(e=>e.Ecode==id);

            if (emp != null)
            {
                //display delete confirmation form
                return View("ConfirmDelete",emp);
            }
            return RedirectToAction("Welcome");
        }

        [HttpGet]
        public IActionResult DeleteById(int id)
        {
            //delete the record by id
            var emp = Employee.Employees.Find(e => e.Ecode == id);

            if (emp != null)
            {
                //delete the record
                Employee.Employees.Remove(emp);
            }
            return RedirectToAction("Welcome");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            //find the record by id
            var emp = Employee.Employees.Find(e => e.Ecode == id);

            if (emp != null)
            {
                return View(emp);
            }
            return RedirectToAction("Welcome");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //find the record by id for edit
            var emp = Employee.Employees.Find(e => e.Ecode == id);

            if (emp != null)
            {
                return View(emp);
            }
            return RedirectToAction("Welcome");
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            //find the record for update
            var emp=Employee.Employees.Find(o=>o.Ecode==employee.Ecode);
            if (emp != null)
            {
                emp.Ename=employee.Ename;
                emp.Salary=employee.Salary;
                emp.Deptid=employee.Deptid;
            }
            return RedirectToAction("Welcome");
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            var customer = new Customer
            {
                Hobbies = new List<string> { "Singing","Painting","Dancing"}
            };
            return View(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            return View("DisplayCustomer",customer);
        }

        [AcceptVerbs("Get","Post")]
        public IActionResult CheckSalaryRange(int salary)
        {
            if(salary<1000 || salary>2000)
            {
                return Json("salary must be between 1000 and 2000");
            }

            return Json(true); //validation passed
        }



        [HttpGet]
        public IActionResult CreateCalc()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCalc(int a ,int b)
        {
            var result = calculator.Add(a , b);
            ViewData.Add("result", result);
            ViewData.Add("demoId1", demo1.id);
            ViewData.Add("demoId2", demo2.id);
            return View("Result");
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
    }
}
