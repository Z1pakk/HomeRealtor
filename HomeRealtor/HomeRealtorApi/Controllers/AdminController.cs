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
        public AdminController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public ContentResult GetAll()
        {
            List<User> useres = _context.Users.ToList();
            string json = null;
            json += JsonConvert.SerializeObject(useres);
            return Content(json, "application/json");
        }

        //[HttpGet("GetRealInfo")]
        //public ContentResult GetRealtorInfo()
        //{
        //   // useres.Where(x => x.UserRoles.FirstOrDefault(y => y.RoleOf.Name == "Realtor") != null)
        //   // List<User> useres = _context.Users.ToList();
        //    //string json = JsonConvert.SerializeObject(useres.Where(u=>u.));
        //    //return Content(json, "application/json");
        //}

        [HttpGet("GetReal")]
        public ContentResult GetRealtor()
        {
            List<User> useres = _context.Users.ToList();
            List<HelpAdminControler> helps = new List<HelpAdminControler>();
            string json = null;
            foreach (var item in useres)
            {
                //if (item.UserRoles.FirstOrDefault(y => y.RoleOf.Name == "Realtor") != null)
                {
                    helps.Add(new HelpAdminControler
                    {
                        Name = item.FirstName + " " + item.LastName,
                        Age = item.Age,
                        Email = item.Email 
                    });
                }
            }
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