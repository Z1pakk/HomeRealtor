using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _sigInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
        }

        [HttpGet("getToken")]
        public async Task<ActionResult<string>> Get([FromBody]UserLoginModel loginModel)
        {

            User user = await _userManager.FindByNameAsync(loginModel.Email);
            var result = await _sigInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return "Error";
            }

            return CreateTokenAsync(user);
        }
        private string CreateTokenAsync(User user)
        {
            var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key-example"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            // Generate the jwt token
            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: now.Add(TimeSpan.FromDays(1))
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}