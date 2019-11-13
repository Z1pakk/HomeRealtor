﻿using System;
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
using HomeRealtorApi.Helpers;

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
        public ContentResult GetRealEstate(string type, int page)
        {
            var elem = new ListAndCount()
            {
                EstatesCount = _context.RealEstates.Count(),
                Estates = _context.RealEstates.
                Skip(page * 3).
                Take(3).
                Where(t => t.SellOf.SellTypeName == type).
                Select(t =>
                new GetListEstateViewModel()
                {
                    Id = t.Id,
                    Image = t.Image,
                    RoomCount = t.RoomCount,
                    StateName = t.StateName,
                    TerritorySize = t.TerritorySize
                }).ToList()
            }; 
           
            string json = JsonConvert.SerializeObject(elem);

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
                    Select(t => new DistrictModel() {Id=t.Id, NameOfDistrict = t.NameOfDistrict, DistrictTypeId=t.DistrictTypeId,TownId=t.TownId }).ToList();

                string json = JsonConvert.SerializeObject(list);

                return Content(json);
            }
            catch (Exception ex)
            {

                return Content("Error: " + ex.Message);
            }
        }
        [HttpGet("get/districts/bytown/{townid}")]
        [Authorize]
        public ContentResult GetDistrictsByTown(int townid)
        {
            try
            {
                var list = _context.Districts.
                    Select(t => new DistrictModel() { Id = t.Id, NameOfDistrict = t.NameOfDistrict, DistrictTypeId = t.DistrictTypeId, TownId = t.TownId }).
                    Where(t=>t.TownId==townid).ToList();

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
                    Select(t => new TownModel() {Id=t.Id, NameOfTown=t.NameOfTown, RegionId=t.RegionId}).ToList();

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
                    Select(t => new RegionModel() {Id=t.Id,NameOfRegion = t.NameOfRegion }).ToList();

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
                Select(t => new DistrictTypeModel() { Id=t.Id,NameOfType = t.NameOfType }).ToList();

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
                Description = estate.Description,
                Coordinates = estate.Coordinates,
                PhoneNumber = estate.UserOf.PhoneNumber,
                Images = estate.ImageEstates?.Select(x => new ImageEstateModel
                {
                    EstateId = x.EstateId,
                    LargeImage = x.LargeImage,
                    MediumImage = x.LargeImage,
                    SmallImage = x.SmallImage
                }).ToList()
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
                    SellType = model.SellType,
                    //HomePlaceId = model.HomePlaceId,
                    Description = model.description
                    
                };
                //foreach (var imgEst in model.images)
                //{
                //    string path = string.Empty;
                //    byte[] imageBytes = Convert.FromBase64String(imgEst.Name);
                //    using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                //    {
                //        //Назва фотки із розширення
                //        path = Guid.NewGuid().ToString() + ".jpg";
                //        Image realEstateImage = Image.FromStream(stream);
                //        realEstateImage.Save(_appEnvoronment.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
                //    }

                //    ImageEstate estateImage = new ImageEstate()
                //    {
                //        //Name = path,
                //        EstateId = estate.Id
                //    };
                //    _context.ImageEstates.Add(estateImage);
                //}
                _context.RealEstates.Add(estate);
                foreach (var imgEst in model.images)
                {
                    string smallImage = string.Empty;
                    string mediumImage = string.Empty;
                    string largeImage = string.Empty;
                    byte[] imageBytes = Convert.FromBase64String(imgEst);
                    using (MemoryStream stream = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        //Назва фотки із розширення
                        string name = Guid.NewGuid().ToString();
                        smallImage = name + "_small.jpg";
                        mediumImage = name + "_medium.jpg";
                        largeImage = name + "_large.jpg";
                        //Image realEstateImage = Image.FromStream(stream);
                        Image imgSmall = ImageHelper.CreateImage((Bitmap)Image.FromStream(stream), 64, 64);
                        Image imgMedium = ImageHelper.CreateImage((Bitmap)Image.FromStream(stream), 480, 480);
                        Image imgLarge = ImageHelper.CreateImage((Bitmap)Image.FromStream(stream), 1024, 1024);
                        imgSmall.Save(_appEnvoronment.WebRootPath + @"/Content/" + smallImage, ImageFormat.Jpeg);
                        imgMedium.Save(_appEnvoronment.WebRootPath + @"/Content/" + mediumImage, ImageFormat.Jpeg);
                        imgLarge.Save(_appEnvoronment.WebRootPath + @"/Content/" + largeImage, ImageFormat.Jpeg);
                        //realEstateImage.Save(_appEnvoronment.WebRootPath + @"/Content/" + path, ImageFormat.Jpeg);
                    }
                    ImageEstate estateImage = new ImageEstate()
                    {
                        SmallImage   = smallImage,
                        MediumImage= mediumImage,
                        LargeImage = largeImage,
                        EstateId = estate.Id
                    };
                    _context.ImageEstates.Add(estateImage);
                }
                estate.Image = estate.ImageEstates.First().MediumImage;


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
                estate.Price = model.Price;
                estate.StateName = model.StateName;
                estate.TerritorySize = model.TerritorySize;
                estate.Location = model.Location;
                estate.TypeId = model.TypeId;
                estate.RoomCount = model.RoomCount;
                estate.SellType = model.SellType;
                //estate.HomePlaceId = model.HomePlaceId;
                estate.Description = model.description;
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
                    query = query.Where(t => t.Price >= from && t.Price <= to);
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
                    foreach (var i in _context.RealEstateTypes)
                    {
                        if (i.TypeName == values[5])
                            query = query.Where(t => t.TypeId == i.Id);
                    }
                }
                if (values[6] != string.Empty)
                {
                    int idRegion = int.Parse(values[6]);
                     query = query.Where(t => t.HomePlaces.FirstOrDefault(g => g.DistrictOf.TownOf.RegionId == idRegion) != null);
                }
                
                if (values[7] != string.Empty)
                {
                    int idTown = int.Parse(values[6]);
                    query = query.Where(t => t.HomePlaces.FirstOrDefault(g => g.DistrictOf.TownId == idTown) != null);
                }
                if (values[8] != string.Empty)
                {
                    int idDistr = int.Parse(values[6]);
                    query = query.Where(t => t.HomePlaces.FirstOrDefault(g => g.DistrictId == idDistr) != null);
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
