﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            if (user.username == "admin" && user.password == "admin123" && user.role=="admin") //use DAL layer to check from DB
            {
                //generate the token if user is valid
                var token = GetToken(user.username, user.password, user.role);
                return Ok(token);
            }
            else
            {
                return BadRequest("invalid");
            }            
        }
        private string GetToken(string username,string password,string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role),
                //new Claim("petname","shitzu")
            };
            var secretKey = config["JWT:Key"];
            var tokenDescriptor = new JwtSecurityToken(            
                issuer : config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                expires:DateTime.UtcNow.AddMinutes(1),
                claims: claims,
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),SecurityAlgorithms.HmacSha256Signature)
                );

            var tokenHandler =new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }
    }
}
