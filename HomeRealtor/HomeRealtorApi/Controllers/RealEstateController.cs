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
                Where(t => t.SellOf.SellTypeName == type).
                Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize,

                }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }

        [HttpGet("get/types")]
        [Authorize]
        public ContentResult GetRealEstateTypes()
        {
            var list = _context.RealEstateTypes.
                Select(t => new TypeViewModel() { Name = t.TypeName, Id = t.Id }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }

        [HttpGet("get/districts")]
        [Authorize]
        public ContentResult GetDistricts()
        {
            try
            {
                var list = _context.Districts.
                    Select(t => new DistrictModel() { NameOfDistrict = t.NameOfDistrict, DistrictTypeId=t.DistrictTypeId,TownId=t.TownId }).ToList();

                string json = JsonConvert.SerializeObject(list);

                return Content(json);
            }
            catch (Exception ex)
            {

                return Content("Error: " + ex.Message);
            }
        }
        [HttpGet("get/towns")]
        [Authorize]
        public ContentResult GetTowns()
        {
            try
            {
                var list = _context.Towns.
                    Select(t => new TownModel() { NameOfTown=t.NameOfTown, RegionId=t.RegionId}).ToList();

                string json = JsonConvert.SerializeObject(list);

                return Content(json);
            }
            catch (Exception ex)
            {

                return Content("Error: " + ex.Message);
            }
        }
        [HttpGet("get/hmpl")]
        [Authorize]
        public ContentResult GetHomePlaces()
        {
            try
            {
                var list = _context.HomePlaces.
                    Select(t => new HomePlaceModel() { DistrictId=t.DistrictId,RealEstateId=t.RealEstateId }).ToList();

                string json = JsonConvert.SerializeObject(list);

                return Content(json);
            }
            catch (Exception ex)
            {

                return Content("Error: " + ex.Message);
            }
        }
        [HttpGet("get/regions")]
        [Authorize]
        public ContentResult GetRegions()
        {
            try
            {
                var list = _context.Regions.
                    Select(t => new RegionModel() {NameOfRegion = t.NameOfRegion }).ToList();

                string json = JsonConvert.SerializeObject(list);

                return Content(json);
            }
            catch (Exception ex)
            {

                return Content("Error: " + ex.Message);
            }
        }
        [HttpGet("get/district/types")]
        [Authorize]
        public ContentResult GetDistrictTypes()
        {
            var list = _context.DistrictTypes.
                Select(t => new DistrictTypeModel() { NameOfType = t.NameOfType }).ToList();

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
                FullName = $"{estate.UserOf?.FirstName} {estate.UserOf?.LastName}"
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
        [HttpPost("find/{type}")]
        [Authorize]
        public ContentResult FindEstates(string type, [FromBody] string[] values)
        {
            try
            {
                var query = _context.RealEstates.AsQueryable();

                if (values[0] != string.Empty && values[1] != string.Empty)
                {
                    double from = double.Parse(values[0]),to= double.Parse(values[1]);
                    query = query.Where(t => t.TerritorySize >=from  && t.TerritorySize <= to); 
                }
                if (values[2] != string.Empty && values[3] != string.Empty)
                {
                    double from = double.Parse(values[2]), to = double.Parse(values[3]);
                    query = query.Where(t => t.TerritorySize >= from && t.TerritorySize <= to);
                }
                if (values[4] != string.Empty)
                {
                    int roomC = int.Parse(values[4]);
                    if (values[4] == "4+")
                        query = query.Where(t => t.RoomCount >= 4);
                    else if (values[4] != "4+")
                        query = query.Where(t => t.RoomCount == roomC);
                }

                if (values[5] != string.Empty)
                {
                    int idRegion = int.Parse(values[5]);
                     query = query.Where(t => t.HomePlaces.FirstOrDefault(g => g.DistrictOf.TownOf.RegionId == idRegion) != null);
                }
                if (values[6] != string.Empty)
                {
                    foreach (var i in _context.RealEstateTypes)
                    {
                        if (i.TypeName == values[5])
                            query = query.Where(t => t.TypeId == i.Id);
                    }
                }
                if (values[7] != string.Empty)
                {
                   // query = query.Where(t => t.HomePlaceOf.Town == values[6]);
                }
                if (values[8] != string.Empty)
                {
                    //query = query.Where(t => t.HomePlaceOf.NameOfDistrict == values[7]);

                }

                var list = query.
              Where(t => t.SellOf.SellTypeName == type).
              Select(t =>
              new GetListEstateViewModel()
              {
                  Id = t.Id,
                  Image = t.Image,
                  RoomCount = t.RoomCount,
                  StateName = t.StateName,
                  TerritorySize = t.TerritorySize,

              }).ToList();
                return Content(JsonConvert.SerializeObject(list));

            }
            catch (Exception rx)
            {
                return Content("Error:" + rx.Message);
            }
        }
    }
}
