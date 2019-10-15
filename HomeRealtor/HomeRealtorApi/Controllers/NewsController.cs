using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly EFContext _context;
     
        [HttpGet("news")]
        public ContentResult GetProducts()
        {
            List<News> products = _context.News.ToList();
            string json = JsonConvert.SerializeObject(products);
            return Content(json, "application/json");
        }
    }
}