using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HomeRealtorApi.Entities;
using HomeRealtorApi.Models;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("get/{type}")]
        public ContentResult GetRealEstate(string type)
        {

            var list = _context.RealEstates.
                Where(t=>t.SellOf.SellTypeName==type).
                Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize
                }).ToList();

           string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }

        [HttpGet("get/types")]
        [Authorize]
        public ContentResult GetRealEstateTypes()
        {
            var list = _context.RealEstateTypes.
                Select(t => new TypeViewModel() {Name = t.TypeName, Id = t.Id }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("get/hmpl")]
        [Authorize]
        public ContentResult GetRealEstateHomePlaces()
        {
            var list = _context.HomePlaces.
                Select(t => new HomePlaceModel() { Town=t.Town,NameOfDistrict=t.NameOfDistrict, Id = t.Id }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("get/hmpl/types")]
        [Authorize]
        public ContentResult GetRealEstateHomePlaceTypes()
        {
            var list = _context.HomePlaces.
                Select(t => new HomePlaceTypeModel() { Id = t.Id, NameOfType=t.NameOfDistrict }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("get/selltypes")]
        public ContentResult GetRealEstateSellTypes()
        {
            var list = _context.RealEstateSellTypes.
                Select(t => new TypeViewModel() { Name = t.SellTypeName, Id = t.Id }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }

        [HttpGet("get/byid/{_id}")]
        public ContentResult GetRealEstate(int _id)
        {           
            RealEstate estate = _context.RealEstates.FirstOrDefault(x => x.Id == _id);
            GetRealEstateViewModel model = new GetRealEstateViewModel()
            {
                Id = estate.Id,
                Active = estate.Active,
                Image = estate.Image,
                Location = estate.Location,
                Price = estate.Price,
                RoomCount = estate.RoomCount,
                StateName = estate.StateName,
                TerritorySize = estate.TerritorySize,
                TimeOfPost = estate.TimeOfPost,
                TypeName = estate.TypeOf?.TypeName,
                FullName = $"{estate.UserOf?.FirstName} {estate.UserOf?.LastName}",
                IsDeleted=estate.IsDeleted
            };
            string estateJson = JsonConvert.SerializeObject(model);
            return Content(estateJson);
        }

        // POST api/values/realestate/add
        [HttpPost("add")]
        public ContentResult AddRealEstate([FromBody]RealEstateViewModel model)
        {
            try
            {
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
                    TimeOfPost = model.TimeOfPost,
                    RoomCount = model.RoomCount,
                    SellType = model.SellType
                };
                _context.RealEstates.Add(estate);
                foreach (var imgEst in model.images)
                {
                    string path = string.Empty;
                    byte[] imageBytes = Convert.FromBase64String(imgEst.Name);
                    using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        //Назва фотки із розширення
                        path = Guid.NewGuid().ToString() + ".jpg";
                        Image realEstateImage = Image.FromStream(stream);
                        realEstateImage.Save(_appEnvoronment.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
                    }

                    ImageEstate estateImage = new ImageEstate()
                    {
                        Name = path,
                        EstateId = estate.Id
                    };
                    _context.ImageEstates.Add(estateImage);
                }
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
                estate.Location = model.Location;
                estate.TerritorySize = model.TerritorySize;
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
        [HttpGet("get")]
        public ContentResult GetRealEstates()
        {

            var list = _context.RealEstates.
                Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize,
                    Active=t.Active,
                    IsDeleted=t.IsDeleted
                }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("getDeleted")]
        public ContentResult GetRealEstatesDeleted()
        {

            var list = _context.RealEstates.Where(t=>t.IsDeleted==true)
                .Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize,
                    Active = t.Active,
                    IsDeleted = t.IsDeleted
                }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("getActive")]
        public ContentResult GetRealEstatesActive()
        {

            var list = _context.RealEstates.Where(t => t.Active == true).Where(t=> t.IsDeleted==false)
                .Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize,
                    Active = t.Active,
                    IsDeleted = t.IsDeleted
                }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("getSold")]
        public ContentResult GetRealEstatesSold()
        {

            var list = _context.RealEstates.Where(t => t.Active == false).Where(t => t.IsDeleted == false)
                .Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize,
                    Active = t.Active,
                    IsDeleted = t.IsDeleted
                }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpDelete("del/{id}")]
        public ContentResult DelRealEstate(int id)
        {
            try
            {
                 _context.RealEstates.FirstOrDefault(x => x.Id == id).IsDeleted=true;
                _context.SaveChanges();
                return Content("Real Estate is deleted");
            }
            catch (Exception ex)
            {
                return Content("Error" + ex.Message);
            }
        }
        [HttpGet("restore/{id}")]
        public ContentResult RestoreRealEstate(int id)
        {
            try
            {
                _context.RealEstates.FirstOrDefault(x => x.Id == id).IsDeleted = false;
                _context.SaveChanges();
                return Content("Real Estate is restored");
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
