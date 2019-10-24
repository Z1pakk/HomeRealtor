using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading;
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
        [HttpPut("edit/{id}")]
        public ContentResult Edit(string id,[FromBody]UserModel User)
        {
            try
            {
                var edit = _context.Users.FirstOrDefault(t => t.Id == id);
                edit.Image=User.Image;
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

                return Content( "Еррор:"+ex.Message);

            }
            
        }
        [HttpGet("unlock/{code}")]
        public async Task<ActionResult<string>> UnlockUser(string code)
        {
            UserUnlockCodes uuc= _context.UserUnlockCodes.FirstOrDefault(t => t.Code == code);
            User user = await _userManager.FindByIdAsync(uuc.UserId);
            _context.Users.FirstOrDefault(t => t.Email == user.Email).CountOfLogins=0;
            await _context.SaveChangesAsync();
            await _userManager.SetLockoutEnabledAsync( user,false);
            return "All done !";
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginModel loginModel)
        {

            // User User = _context.Users.FirstOrDefault(t => t.Email == loginModel.Email);
            
            
           try
            {

                User user  = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins++;
                    await _context.SaveChangesAsync();
                    return "Error";
                }
                var result = await _sigInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (user.CountOfLogins >= 10)
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                string code = Guid.NewGuid().ToString();
                _context.UserUnlockCodes.Add(new UserUnlockCodes()
                {
                    Code = code,
                    UserId = user.Id
                });
                mail.From = new MailAddress("home.realtor.suport@gmail.com");
                mail.To.Add(user.Email);
                mail.Subject = "Unlock account";
                mail.IsBodyHtml = true;
                mail.Body = "" +
                "<head>" +
                "Your account is locked press button to unlock :" +
                "</head>"+
                $" <a href=\"localhost:54365/api/user/unlock/{code}\">" +
                "<button>" +
                "Unlock" +
                "</button>" +
                " </a>  ";
             


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("home.realtor.suport@gmail.com", "00752682");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
    
                await _userManager.SetLockoutEnabledAsync(user, true );
                return "Locked";
            }


               // List<string> role =(List<string>)await _userManager.GetRolesAsync(user);
            if(await _userManager.IsLockedOutAsync(user))
            {
                
                return "Locked";
            }
            //TODO: FindByPhoneAsync
            //if(user==null)
            //{
            //    user= await _userManager.FindByPhoneAsync(loginModel.Email);
            //}
            
            
            if (!result.Succeeded)
            {
                _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins++;
                await _context.SaveChangesAsync();
                return "Error";
            }
                _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins=0;
                return   CreateTokenAsync(user/*,role[0]*/);
            }
            catch(Exception ex)
            {
                _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins++;
                await _context.SaveChangesAsync();
                return "Error";
            }

            
        }

        private string CreateTokenAsync(User user/*,string role*/)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.Id),
               // new Claim("role",role)
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