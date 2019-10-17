using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeRealtorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly EFContext _context;
        public RealEstateController(EFContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ContentResult GetRealEstate()
        {
            /*List<RealEstate> estates= new List<RealEstate>();
            foreach (var estate in _context.RealEstates)
                estates.Add(estate);
            string estateJson = JsonConvert.SerializeObject(estates);*/

            string json = JsonConvert.SerializeObject( _context.RealEstates.ToList());

            return Content(json);
        }

        // GET api/values/get/realEstate/5
        [HttpGet("get/{id}")]
        public ActionResult<string> GetRealEstate(int id)
        {
            RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == id);
            string estateJson = JsonConvert.SerializeObject(estate);
            return estateJson;
        }

        // POST api/values/add/realEstate
        [HttpPost("add")]
        public ContentResult AddRealEstate([FromBody]RealEstateViewModel model)
        {
            try
            {
                RealEstate estate = new RealEstate()
                {
                    Active = model.Active,
                    Image = model.Image,
                    ImageEstates = model.ImageEstates,
                    Price = model.Price,
                    StateName = model.StateName,
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
                estate.ImageEstates = model.ImageEstates;
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
