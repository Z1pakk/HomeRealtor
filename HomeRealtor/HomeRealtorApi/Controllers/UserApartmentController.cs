using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApartmentController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public UserApartmentController(EFContext context, IHostingEnvironment appEnvironment)
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

        [HttpPost("add")]
        public ContentResult AddOrder()
        {
            return Content("hiboy");
        }


    }
}