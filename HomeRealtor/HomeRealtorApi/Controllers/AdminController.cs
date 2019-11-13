using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _sigInManager;
        public AdminController(EFContext context, UserManager<User> userManager, SignInManager<User> user)
        {
            _context = context;
            _userManager = userManager;
            _sigInManager = user;
        }

        [HttpGet("getall")]
        public ContentResult GetAll()
        {
            List<User> useres = _context.Users.ToList();
            string json = null;
            json += JsonConvert.SerializeObject(useres);
            return Content(json, "application/json");
        }

        [HttpGet("GetRealInfo")]
        public async Task<ContentResult> GetRealtorInfo()
        {
            List<User> useres = (List<User>)await _userManager.GetUsersInRoleAsync("REALTOR");
            string json = JsonConvert.SerializeObject(useres);
            return Content(json, "application/json");
        }

        [HttpGet("GetUser")]
        public async Task<ContentResult> GetUserInfo()
        {
            List<User> useres = (List<User>)await _userManager.GetUsersInRoleAsync("USER");
            string json = JsonConvert.SerializeObject(useres);
            return Content(json, "application/json");
        }

        [HttpGet("GetReal")]
        public async Task<ContentResult> GetRealtor()
        {
            List<User> useres = (List<User>)await _userManager.GetUsersInRoleAsync("REALTOR");
            List<HelpAdminControler> helps = new List<HelpAdminControler>();
            string json = null;
            foreach (var item in useres)
            {
                helps.Add(new HelpAdminControler
                {
                    Name = item.FirstName + " " + item.LastName,
                    Age = item.Age,
                    Email = item.Email
                });

            }
            json = JsonConvert.SerializeObject(helps);
            return Content(json, "application/json");
        }

        [HttpGet("GetUserPagin/{value}")]
        public ContentResult GetUserPagination(int value)
        {
            var useres = _context.Users;

            var helps = useres.Select(p => new HelpAdminControler
            {
                Name = p.FirstName + " " + p.LastName,
                Age = p.Age,
                Email = p.Email,
                AboutMy = p.AboutMe,
                Path = @"https://localhost:44325/Content/Users/"+p.Image
            }).Skip((value - 1) * 10).Take(10).ToList();
            string json = JsonConvert.SerializeObject(helps);
            return Content(json, "application/json");
        }


        [HttpGet("ban/{email}")]
        public async Task<ActionResult<string>> BanUserAsync(string email)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(t => t.Email == email);
                await _userManager.SetLockoutEnabledAsync(user, true);
                return "User Baned";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody]UserLoginModel loginModel)
        {

            // User User = _context.Users.FirstOrDefault(t => t.Email == loginModel.Email);


            try
            {

                User user = await _userManager.FindByEmailAsync(loginModel.Email);
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
                    "</head>" +
                    $" <a href=\" https://localhost:44325/api/user/unlock/{code}/ \">" +
                    "<button>" +
                    "Unlock" +
                    "</button>" +
                    " </a>  ";



                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("home.realtor.suport@gmail.com", "00752682");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                    await _userManager.SetLockoutEnabledAsync(user, true);
                    return "Locked";
                }

                // List<string> role =(List<string>)await _userManager.GetRolesAsync(user);
                if (await _userManager.GetLockoutEnabledAsync(user))
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
                _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins = 0;
                return CreateTokenAsync(user/*,role[0]*/);
            }
            catch (Exception ex)
            {
                _context.Users.FirstOrDefault(t => t.Email == loginModel.Email).CountOfLogins++;
                await _context.SaveChangesAsync();
                return "Error";
            }

            //return CreateTokenAsync(user,role[0]);


        }

        private string CreateTokenAsync(User user/*,string role*/)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
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