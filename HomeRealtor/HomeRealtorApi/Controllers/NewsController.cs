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
        public NewsController(EFContext context)
        {
            _context = context;
        }

        [HttpGet("news_")]
        public ContentResult GetProducts()
        {
            List<News> news = _context.News.ToList();
            string json = JsonConvert.SerializeObject(news);
            return Content(json, "application/json");
        }
    }
}