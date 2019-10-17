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

        private readonly EFContext _context;

        public UserController(EFContext context, UserManager<User> userManager, SignInManager<User> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _context = context;
        }
        [HttpPost("add")]
        public async Task<ActionResult<string>> Add([FromBody]UserModel User)
        {

            User user = new User()
            {
               
                Email = User.Email,
                Age = User.Age,
                UserName = User.UserName,
                PhoneNumber =User.PhoneNumber,
                FirstName = User.FirstName,
                AboutMe=User.AboutMe,
                LastName = User.LastName
            };

            var result = await _userManager.CreateAsync(user, User.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return "Еррор:";
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginModel loginModel)
        {

            User user = await _userManager.FindByEmailAsync(loginModel.Email);
            //TODO: FindByPhoneAsync
            //if(user==null)
            //{
            //    user= await _userManager.FindByPhoneAsync(loginModel.Email);
            //}
            if (user == null)
            {
                return "Error";
            }
            var result = await _sigInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (!result.Succeeded)
            {
                return "Error";
            }

            return user.Id;
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