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
    public class ImageEstateController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly IHostingEnvironment _appEnvoronment;
        public ImageEstateController(EFContext context, IHostingEnvironment appEnvoronment)
        {
            _context = context;
            _appEnvoronment = appEnvoronment;
        }
        // GET api/values
        [HttpGet("get")]
        public ContentResult Get()
        {
            List<string> estatesJson = new List<string>();
            string json = JsonConvert.SerializeObject(_context.ImageEstates);
            return Content(json);
        }

        // GET api/values/5
        [HttpGet("get/{id}")]
        public ContentResult Get(int id)
        {
            ImageEstate estate = _context.ImageEstates.FirstOrDefault(x => x.Id == id);
            string estateJson = JsonConvert.SerializeObject(estate);
            return Content(estateJson);
        }

        // POST api/values
        [HttpPost("add")]
        public ContentResult AddImageEstate([FromBody]ImageEstateModel model)
        {
            try
            {
                string path = string.Empty;
                byte[] imageBytes = Convert.FromBase64String(model.Name);
                using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    //Назва фотки із розширення
                    path = Guid.NewGuid().ToString() + ".jpg";
                    Image realEstateImage = Image.FromStream(stream);
                    realEstateImage.Save(_appEnvoronment.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
                }

                ImageEstate estate = new ImageEstate()
                {
                    Name = path,
                    EstateId = model.EstateId
                };
                _context.ImageEstates.Add(estate);
                _context.SaveChanges();
                return Content("Image Estate is added");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }


        // PUT api/values/5
        [HttpPut("edit/{id}")]
        public ContentResult EditImageEstate(int id, [FromBody]ImageEstateModel model)
        {
            try
            {

                ImageEstate estate = _context.ImageEstates.FirstOrDefault(x => x.Id == id);
                estate.Name = model.Name;
                estate.EstateId = model.EstateId;
                _context.SaveChanges();
                return Content("Image Estate is edited");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ContentResult DeleteImageEstate(int id)
        {
            try
            {

                ImageEstate estate = _context.ImageEstates.FirstOrDefault(x => x.Id == id);
                _context.ImageEstates.Remove(estate);
                _context.SaveChanges();
                return Content("Image Estate is deleted");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }
    }
}
