using ADODemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ADOLib;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ADODemoMVC.Controllers
{
    [Authorize]
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
            var token = HttpContext.Session.GetString("token");
            var dtoEmps = consumer.GetAllEmps(token);
            //map the result using DTO
            var lstEmps=mapper.Map<List<Employee>>(dtoEmps);

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
                var token = HttpContext.Session.GetString("token");
                //insert the record
                var status =consumer.AddEmployee(employee,token);
                if (status == true)
                    return RedirectToAction("Index");
                else
                {
                    ViewData.Add("errMsg", "could not insert record");
                    return View();
                }
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
            var orderId = 0;
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
