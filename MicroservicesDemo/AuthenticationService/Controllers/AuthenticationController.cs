using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration config;
        public AuthenticationController(IConfiguration config)
        {
            this.config=config;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel user)
        {
            //validate user from Database - TODO
            if (user.Username == "admin" && user.Password == "admin123")
            {
                //generate token
                var token=GenerateToken(user);
                return Ok(token);
            }
            return null;
        }

        private string GenerateToken(LoginModel user)
        {
            var token = "";
            //use jwt token handler to generate token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                //new Claim(ClaimTypes.Role, role),
                //new Claim("petname","shitzu")
            };
            var secretKey = config["JWT:Key"];
            var tokenDescriptor = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }
    }
}
