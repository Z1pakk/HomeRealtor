using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Helpers;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public OrderController(EFContext context, IHostingEnvironment appEnvironment, UserManager<User> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        [HttpGet("orders")]
        public ContentResult getOrders()
        {
            List<Order> orders = _context.Orders.ToList();
            string json = JsonConvert.SerializeObject(orders);

            return Content(json, "application/json");
        }

        [HttpDelete("delete/{id}")]
        public ContentResult DeleteOrder(int id)
        {
            _context.Orders.Remove(_context.Orders.FirstOrDefault(t => t.Id == id));
            return Content("OK");
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ContentResult> AddOrderAsync([FromBody]AddOrderModel model)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == model.ApartId);
                Order order = new Order()
                {
                    ApartId = estate.Id,
                    Status = estate.Active,
                    UserId = user.Id,
                    RealtorId = estate.UserId,
                    Message = model.Message
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                ApiResponse APIResponse = new ApiResponse()
                {
                    Success = true,
                    Result = "Your Order was sent!"
                };
                return Content(JsonConvert.SerializeObject(APIResponse), "application/json");
            }
            catch (Exception ex)
            {
                ApiResponse APIResponse = new ApiResponse()
                {
                    Success = false,
                    ExceptionMessage = ex.Message
                };
                return Content(JsonConvert.SerializeObject(APIResponse), "application/json");
            }
        }


    }
}