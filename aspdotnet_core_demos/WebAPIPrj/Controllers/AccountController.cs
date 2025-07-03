using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using WebAPIPrj.Models;

namespace WebAPIPrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration config;
        public AccountController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserDetails user)
        {           
            if (user.username == "admin" && user.password == "admin123") //use DAL layer to check from DB
            {
                //generate the token if user is valid
                var token = GetToken(user.username, user.password);
                return Ok(token);
            }
            else
            {
                return null;
            }            
        }
        private string GetToken(string username,string password)
        {
            var secretKey = config["JWT:Key"];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = config["JWT:Issuer"],
                Audience= config["JWT:Audience"],
                Expires=DateTime.UtcNow.AddMinutes(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler =new JwtSecurityTokenHandler();
            var token =tokenHandler.CreateToken(tokenDescriptor);
        
            return tokenHandler.WriteToken(token);
        }
    }
}
