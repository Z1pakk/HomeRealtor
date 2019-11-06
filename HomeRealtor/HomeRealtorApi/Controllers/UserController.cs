using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RealtorUI.Models;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _sigInManager;

        private readonly EFContext _context;

        private readonly IHostingEnvironment hosting;

        public UserController(EFContext context, UserManager<User> userManager, SignInManager<User> sigInManager)
        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _context = context;
        }

        [HttpPost("add")]
        public async Task<ActionResult<string>> Add([FromBody]UserModel User)
        {
            try
            {
                string path = "";
                if (User.Image != null)
                {
                    byte[] imageBytes = Convert.FromBase64String(User.Image);
                    using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        path = Guid.NewGuid().ToString() + ".jpg";
                        Image product = Image.FromStream(stream);
                        product.Save(hosting.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
                    }
                }
                User user = new User()
                {
                    UserName = User.UserName,
                    Email = User.Email,
                    Age = User.Age,
                    PhoneNumber = User.PhoneNumber,
                    FirstName = User.FirstName,
                    AboutMe = User.AboutMe,
                    LastName = User.LastName,
                    Image = path
                };



                var result = await _userManager.CreateAsync(user, User.Password);
                await _userManager.AddToRoleAsync(user, "Realtor");
                await _userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded)
                {
                    return Ok();
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
        //[HttpPut("edit/{id}")]
        //public ContentResult Edit(string id, [FromBody]UserModel User)
        //{
        //    try
        //    {
        //        var edit = _context.Users.FirstOrDefault(t => t.Id == id);
        //        edit.Image = User.Image;
        //        edit.LastName = User.LastName;
        //        edit.PhoneNumber = User.PhoneNumber;
        //        edit.UserName = User.UserName;
        //        edit.FirstName = User.FirstName;
        //        edit.AboutMe = User.AboutMe;
        //        edit.Age = User.Age;
        //        edit.Email = User.Email;
        //        _context.SaveChanges();
        //        return Content("OK");
        //    }
        //    catch (Exception ex)
        //    {

        //        return Content("Еррор:" + ex.Message);

        //    }

        //}
        [HttpPut("change")]
        [Authorize]
        public async Task<ContentResult> ChangePasswordAsync([FromBody]string []Passwords)
        {
            try
            {
                User us = _context.Users.FirstOrDefault(t => t.UserName == this.User.Identity.Name);
                IdentityResult res=await _userManager.ChangePasswordAsync(us, Passwords[0], Passwords[1]);
                return Content("OK");
            }
            catch (Exception ex)
            {
                return Content("Еррор:" + ex.Message);

            }

        }
        [HttpGet("current")]
        [Authorize]
        public async Task<ContentResult> CurrentUser()
        {
            try
            {
                
                //_userManager.FindByNameAsync(this.User.Identity.Name);
                User us  =_context.Users.FirstOrDefault(t => t.UserName == this.User.Identity.Name);
                string json = JsonConvert.SerializeObject(new UserInfoModel()
                {
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    AboutMe = us.AboutMe,
                    Age = us.Age,
                    Email = us.Email,
                    Image = us.Image,
                    PhoneNumber = us.PhoneNumber,
                    Id = us.Id,
                    UserName = us.UserName,
                });

                return Content(json);
            }
            catch (Exception ec)
            {
                return Content("Error: " + ec.Message);
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


                List<string> role =(List<string>)await _userManager.GetRolesAsync(user);
                if(!role.Contains(loginModel.Role))
                {
                    return "Role";
                }
                if (await _userManager.IsLockedOutAsync(user))
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

            //  return  CreateTokenAsync(user,role[0]);


        }

        [HttpPost("sendcode")]
        public async Task<IActionResult> SendCode([FromBody]SendCodeModel model)
        {
            try
            {
                string email = model.Email;

                User user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest();
                }
                string code = string.Empty;
                var forgotpassword = _context.ForgotPasswords.FirstOrDefault(t => t.UserId == user.Id);
                if (forgotpassword == null)
                {
                    code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    ForgotPassword password = new ForgotPassword()
                    {
                        Code = code,
                        UserId = user.Id
                    };
                    _context.ForgotPasswords.Add(password);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    code = forgotpassword.Code;
                }



                MailAddress to = new MailAddress(email);
                MailAddress from = new MailAddress("homerealtor@gmail.com", "Home Realtor");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Input this code :";
                m.IsBodyHtml = true;
                m.Body = "Code : " + code + " .";



                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("home.realtor.suport@gmail.com", "00752682");
                smtp.EnableSsl = true;
                smtp.Send(m);



                return Content("OK");



            }
            catch (Exception)
            {



                return BadRequest();
            }
        }

        [HttpGet("checkcode")]
        public ContentResult CheckCode([FromBody]CheckCodeModel model)
        {
           var res = _context.ForgotPasswords.FirstOrDefault(t => t.Code == model.Code);
           if(res!=null)
            {
                _userManager.ResetPasswordAsync(res.UserOf, model.Code, model.NewPassword);
            }
           

            return Content("OK");
        }

        private string CreateTokenAsync(User user/*, string role*/)
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