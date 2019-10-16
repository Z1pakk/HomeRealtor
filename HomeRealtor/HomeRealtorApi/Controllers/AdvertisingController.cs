using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisingController : ControllerBase
    {
        private readonly EFContext _context;
        public AdvertisingController(EFContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]AdvertisingModel Advertising)
        {
            try
            {
                Advertising advertising = new Advertising()
                {
                    StateName = Advertising.StateName,
                    Price = Advertising.Price,
                    Image = Advertising.Image,
                    Contacts = Advertising.Contacts,
                    UserId = Advertising.UserId
                };

                var result = _context.Advertisings.Add(advertising);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("advertising_")]
        public ContentResult GetProducts()
        {
            List<Advertising> advertisings = _context.Advertisings.ToList();
            string json = JsonConvert.SerializeObject(advertisings);
            return Content(json, "application/json");
        }
    }
}