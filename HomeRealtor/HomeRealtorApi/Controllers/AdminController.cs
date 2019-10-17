﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using Microsoft.AspNetCore.Hosting;
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

        [HttpGet("GetRealInfo")]
        public ContentResult GetRealtorInfo()
        {
            List<User> useres = _context.Users.ToList();
            string json = JsonConvert.SerializeObject(useres.Where(x => x.UserRoles.FirstOrDefault(y=>y.RoleOf.Name=="Realtor")!=null));
            return Content(json, "application/json");
        }

        [HttpGet("GetReal")]
        public ContentResult GetRealtor()
        {
            List<User> useres = _context.Users.ToList();
            string json = null;
            foreach (var item in useres)
            {
                if(item.UserRoles.FirstOrDefault(y => y.RoleOf.Name == "Realtor") != null)
                    json += JsonConvert.SerializeObject(item.FirstName + " " + item.LastName);
            }
            return Content(json, "application/json");
        }

        //Delete User
        /*[HttpDelete("delete/{id}")]
        public ContentResult DeleteProduct(int id)
        {
            try
            {
                var seekUser = _context.User.FirstOrDefault(e => e.Id == id);
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
            }
            catch (Exception ex)
            {
                APIResponse api = new APIResponse()
                {
                    Success = false,
                    Result = ex.Message
                };
                return Content(JsonConvert.SerializeObject(api), "application/json");
            }

        }*/

        //edit
        /* [HttpPut("edit/{id}")]
         public ContentResult EditProduct(int id, [FromBody]User model)
         {
             try
             {
                 var seekUser = _context.User.FirstOrDefault(e => e.Id == id);
                 if (seekUser.Id != 0)
                 {


                     //ToDo  seekUser. Something = model. Something
                     _context.SaveChanges();


                     return Content("Edit complate");
                 }
                 else
                 {
                     return Content("ERORA");
                 }

             }
             catch (Exception ex)
             {
                 return Content(ex.Message);
             }
         }*/
    }
}