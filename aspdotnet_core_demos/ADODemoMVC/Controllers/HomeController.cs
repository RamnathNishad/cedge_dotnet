using ADODemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ADOLib;
using AutoMapper;

namespace ADODemoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        ApiConsumer consumer;
        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;
            consumer = new ApiConsumer();
        }
        public IActionResult Index()
        {
            var dtoEmps = consumer.GetAllEmps();
            //map the result using DTO
            var lstEmps=mapper.Map<List<Employee>>(dtoEmps);

            //var totalSalary=dal.GetTotalSalary();
            ViewBag.TotalSalary = 0;// totalSalary;

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
                //dal.AddEmployee(employee);
                consumer.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dtoEmp = consumer.GetEmpById(id);
            //map the result using DTO
            var emp = mapper.Map<Employee>(dtoEmp);
            return View(emp);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //dal.DeleteEmployee(id);
            consumer.DeleteEmp(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //find the record and edit
            var dtoEmp = consumer.GetEmpById(id);
            //map the result using DTO
            var emp = mapper.Map<Employee>(dtoEmp);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            //update the record
            //dal.UpdateEmployee(employee);
            consumer.UpdateEmp(employee);
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
            var orderId = 0;// dal.PlaceOrder(amount,quantity);
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
                //dal.FundsTransfer(fromAccNo, toAccNo, amount);
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
