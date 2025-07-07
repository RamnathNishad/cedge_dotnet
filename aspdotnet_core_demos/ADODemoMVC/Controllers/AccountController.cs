using ADODemoMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

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
        public IActionResult Login(string username, string password,string role)
        {
            ApiConsumer consumer = new ApiConsumer();
            //call the Account API Authenticate method to get token
            var token = consumer.Authenticate(username, password,role);

            if (token != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("token",token)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                var claimsPrinciple = new ClaimsPrincipal(identity);
                //Sign-in the user with AuthCookie to the browser 
                HttpContext.SignInAsync("MyCookieAuth", claimsPrinciple);
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
           
            HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }
    }
}
