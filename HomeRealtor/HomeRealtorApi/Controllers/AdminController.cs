using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<User> _userManager;
        public AdminController(EFContext context,UserManager<User> userManager)
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

        //Delete User
        /*[HttpDelete("delete/{id}")]
        public ContentResult DeleteUser(int id)
        {
         //   try
          //  {
                var seekUser = _context.Users.FirstOrDefault(e => e.Id == id);
                if (seekUser.Id != 0)
                {
                    _context.Products.Remove(seekUser);
                    _context.SaveChanges();
                    APIResponse api = new APIResponse()
                    {
                        Success = true,
                        Result = "Product deleted"
                    };
                    return Content(JsonConvert.SerializeObject(api), "application/json");
                }
                else
                {
                    return Content("Something wrong");
                }
          //  }
            //catch (Exception ex)
           // {
                //APIResponse api = new APIResponse()
                //{
                //    Success = false,
                //    Result = ex.Message
                //};
                //return Content(JsonConvert.SerializeObject(api), "application/json");
          //  }

        }*/
    }
}