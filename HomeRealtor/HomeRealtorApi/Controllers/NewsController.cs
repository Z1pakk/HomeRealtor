using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Model;
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
        
        [HttpGet("news")]
        public ContentResult GetProducts()
        {
           
            List<News> news = _context.News.ToList();
            string json = JsonConvert.SerializeObject(news);
           return Content(json, "application/json");
        }
        [HttpPost("add")]
        public ContentResult AddNews([FromBody] AddNewsViewModel model)
        {
            try
            {
                News news = new News()
                {
                    Headline = model.Headline,
                    Text = model.Text
                };
                _context.News.Add(news);
                _context.SaveChanges();

                return Content("News Add!");

            }
            catch (Exception ex)
            {

                return Content("Помилка:" + ex.Message);
            }
            
        }
    }
}