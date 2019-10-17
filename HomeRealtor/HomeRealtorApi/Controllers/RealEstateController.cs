using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvoronment;
        public RealEstateController(EFContext context, IHostingEnvironment appEnvoronment)
        {
            _context = context;
            _appEnvoronment = appEnvoronment;
        }
        // GET api/values
        [HttpGet]
        public ContentResult GetRealEstate()
        {

            string json = JsonConvert.SerializeObject( _context.RealEstates.ToList());

            return Content(json);
        }

        // GET api/values/get/realEstate/5
        [HttpGet("get/{id}")]
        public ContentResult GetRealEstate(int id)
        {
            RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == id);
            string estateJson = JsonConvert.SerializeObject(estate);
            return Content(estateJson);
        }

        // POST api/values/realestate/add
        [HttpPost("add")]
        public ContentResult AddRealEstate([FromBody]RealEstateViewModel model)
        {
            try
            {
                string path = string.Empty;

                RealEstate estate = new RealEstate()
                {
                    Active = model.Active,
                    Image = model.Image,
                    Price = model.Price,
                    StateName = model.StateName,
                    TerritorySize = model.TerritorySize,
                    Location = model.Location,
                    TypeId = model.TypeId,
                    UserId = model.UserId,
                    TimeOfPost = model.TimeOfPost
                };

                _context.RealEstates.Add(estate);
                _context.SaveChanges();
                return Content("Real Estate is added");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }

        // PUT api/values/edit/realEstate/5
        [HttpPut("edit/{id}")]
        public ContentResult EditRealEstate(int id, [FromBody]RealEstateViewModel model)
        {
            try
            {
                RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == id);
                estate.Active = model.Active;
                estate.Image = model.Image;
                estate.Price = model.Price;
                estate.StateName = model.StateName;
                estate.TypeId = model.TypeId;
                estate.UserId = model.UserId;
                estate.TimeOfPost = model.TimeOfPost;
                _context.SaveChanges();
                return Content("Real Estate is edited");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("delete/{id}")]
        public ContentResult DeleteRealEstate(int id)
        {
            try
            {
                RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == id);

                _context.RealEstates.Remove(estate);
                _context.SaveChanges();
                return Content("Real Estate is deleted");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }
    }
}
