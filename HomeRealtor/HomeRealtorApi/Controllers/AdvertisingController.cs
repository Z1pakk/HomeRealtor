using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConnectService.Service;
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
                    UserId = Advertising.UserId,
                    RealEstsateId = Advertising.RealEstateId
                };


                var result = _context.Advertisings.Add(advertising);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("advertising")]
        public ContentResult GetProducts()
        {
            List<Advertising> advertisings = _context.Advertisings.ToList();
            List<AdvertisingModel> models = new List<AdvertisingModel>();
            foreach(var item in advertisings)
            {
                AdvertisingModel model = new AdvertisingModel()
                {
                    UserId = item.UserId,
                    RealEstateId = item.RealEstsateId,
                    Image = item.RealEstateOf.Image,
                    Contacts = item.UserOf.Email,
                    StateName = item.RealEstateOf.StateName
                };
                models.Add(model);
            }

            string json = JsonConvert.SerializeObject(models);
            return Content(json, "application/json");
        }
    }
}