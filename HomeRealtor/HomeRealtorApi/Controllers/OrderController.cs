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
        public OrderController(EFContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
        public ContentResult AddOrder([FromBody]AddOrderModel model)
        {
            try
            {
                Order order = new Order()
                {
                    ApartId = model.ApartId,
                    Status = model.Status,
                    UserId = model.UserId,
                    RealtorId = model.RealtorId
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                ApiResponse APIResponse = new ApiResponse()
                {
                    Success = true,
                    Result = "PLUS ORDER"
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