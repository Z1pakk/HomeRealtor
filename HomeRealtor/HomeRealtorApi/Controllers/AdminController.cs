﻿using System;
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
using RealtorUI.Models;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<User> _userManager;
        public AdminController(EFContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            string json = null;
            var helps = useres.Select(p => new HelpAdminControler
            {
                Name = p.FirstName + " " + p.LastName,
                Age = p.Age,
                Email = p.Email
            }).ToList().Skip((value - 1) * 10).Take(10);
            json = JsonConvert.SerializeObject(helps);
            return Content(json, "application/json");
        }


        [HttpGet("ban/{code}")]
        public async Task<ActionResult<string>> BanUserAsync(string code)
        {
            UserUnlockCodes uuc = _context.UserUnlockCodes.FirstOrDefault(t => t.Code == code);
            User user = await _userManager.FindByIdAsync(uuc.UserId);
            await _userManager.SetLockoutEnabledAsync(user, true);
            return "User Baned";
        }

        [HttpPost("sendcode")]
        public async Task<ContentResult> SendCode([FromBody]SendCodeModel model)
        {
            string email = model.Email;
            Random rnd = new Random();
            string code = (rnd.Next(1000, 9999)).ToString();
            User user = await _userManager.FindByEmailAsync(email);

            ForgotPassword password = new ForgotPassword()
            {
                Code = code,
                UserId = user.Id
            };
            _context.ForgotPasswords.Add(password);
            _context.SaveChanges();

            MailAddress to = new MailAddress(email);
            MailAddress from = new MailAddress("homerealtor@gmail.com", "Home Realtor");
            MailMessage m = new MailMessage(from, to);
            string _code = await _userManager.GeneratePasswordResetTokenAsync(user);
            password.Code.Replace(code, _code);
            _context.SaveChanges();
            m.Subject = "Input this code :";
            m.IsBodyHtml = true;
            m.Body = "Code : " + _code + " .";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("homerealtor@gmail.com", "homeRealtor1234");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return Content("OK");
        }



        [HttpGet("checkcode")]
        public ContentResult CheckCode([FromBody]CheckCodeModel model)
        {
            var res = _context.ForgotPasswords.FirstOrDefault(t => t.Code == model.Code);
            if (res != null)
            {
                _userManager.ResetPasswordAsync(res.UserOf, model.Code, model.NewPassword);
            }


            return Content("OK");
        }
    }
}