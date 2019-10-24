using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Hosting;
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

        private readonly IHostingEnvironment _appEnvoronment;

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
                UserName = User.UserName,
                Email = User.Email,
                Age = User.Age,
                PhoneNumber = User.PhoneNumber,
                FirstName = User.FirstName,
                AboutMe = User.AboutMe,
                LastName = User.LastName,
                Image = User.Image
            };

            string path = string.Empty;
            byte[] imageBytes = Convert.FromBase64String(User.Image);
            using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                //Назва фотки із розширення
                path = Guid.NewGuid().ToString() + ".jpg";
                Image realEstateImage = Image.FromStream(stream);
                realEstateImage.Save(_appEnvoronment.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
            }

            ImageUser userImage = new ImageUser()
            {
                Name = path,
                UserId = user.Id
            };
            _context.ImageUsers.Add(userImage);


            var result = await _userManager.CreateAsync(user, User.Password);
            await _userManager.AddToRoleAsync(user, User.Role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("edit/{id}")]
        public ContentResult Edit(string id, [FromBody]UserModel User)
        {
            try
            {
                var edit = _context.Users.FirstOrDefault(t => t.Id == id);
                edit.Image = User.Image;
                edit.LastName = User.LastName;
                edit.PhoneNumber = User.PhoneNumber;
                edit.UserName = User.UserName;
                edit.FirstName = User.FirstName;
                edit.AboutMe = User.AboutMe;
                edit.Age = User.Age;
                edit.Email = User.Email;
                _context.SaveChanges();
                return Content("OK");
            }
            catch (Exception ex)
            {

                return Content("Еррор:" + ex.Message);

            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginModel loginModel)
        {

            try
            {
                User user = await _userManager.FindByEmailAsync(loginModel.Email);
                List<string> role = (List<string>)await _userManager.GetRolesAsync(user);
                Thread.Sleep(5000);
                //TODO: FindByPhoneAsync
                //if(user==null)
                //{
                //    user= await _userManager.FindByPhoneAsync(loginModel.Email);
                //}
                if (user == null)
                {
                    return BadRequest();
                }
                var result = await _sigInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                if (!result.Succeeded)
                {
                    return BadRequest();
                }

                return Ok(CreateTokenAsync(user, role[0]));

            }
            catch (Exception)
            {
                return BadRequest();
            }


        }

        private string CreateTokenAsync(User user, string role)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("role",role)
            };
            var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key-example"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
            // Generate the jwt token
            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: now.Add(TimeSpan.FromDays(1)),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}