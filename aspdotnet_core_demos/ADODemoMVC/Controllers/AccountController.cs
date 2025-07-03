using ADODemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ADODemoMVC.Controllers
{
    public class AccountController : Controller
    {  
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            ApiConsumer consumer=new ApiConsumer();
            //call the Account API Authenticate method to get token
            var token = consumer.Authenticate(username,password);

            if(token!=null)
            {
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}
