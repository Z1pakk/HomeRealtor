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

            List<GetNewsViewModel> getnews=new List<GetNewsViewModel>();
            List<News> news=_context.News.ToList();
          GetNewsViewModel getNews = new GetNewsViewModel();
           
            foreach (var item in news)
            {  
                getNews.Headline = item.Headline;
                getNews.Text = item.Text;
                getNews.Id = item.Id;
               getnews.Add(getNews);
            }
           
            
            string json = JsonConvert.SerializeObject(getnews);
            return Content(json, "application/json");
        }

        [HttpGet("get/{id}")]
        public ContentResult Get(int id)
        {

            _context.News.Add(_context.News.FirstOrDefault(t => t.Id == id));
            return Content("OK");
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