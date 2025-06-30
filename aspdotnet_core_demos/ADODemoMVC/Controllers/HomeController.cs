using ADODemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ADOLib;

namespace ADODemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeesRepository dal;
        public HomeController(IEmployeesRepository dal)
        {
            this.dal = dal;
        }
        public IActionResult Index()
        {
            var lstEmps = dal.GetAllEmps();
            var totalSalary=dal.GetTotalSalary();
            ViewBag.TotalSalary = totalSalary;

            return View(lstEmps);
        }

        [HttpGet]
        //[Route("AddEmployee")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                //insert the record
                dal.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var emp=dal.GetEmployee(id);
            return View(emp);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            dal.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //find the record and edit
            var emp=dal.GetEmployee(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            //update the record
            dal.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(int amount,int quantity)
        {
            var orderId=dal.PlaceOrder(amount,quantity);
            ViewBag.msg = "Order placed, ur order id:" + orderId;
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Deposit(int fromAccNo,int toAccNo,int amount)
        {
            try
            {
                dal.FundsTransfer(fromAccNo, toAccNo, amount);
                ViewBag.msg = "Funds transferred successfully!!!";                
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }
    }
}
