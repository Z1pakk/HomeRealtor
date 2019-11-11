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
        [HttpGet("get/hmpl")]
        [Authorize]
        public ContentResult GetRealEstateHomePlaces()
        {
            var list = _context.HomePlaces.
                Select(t => new HomePlaceModel() { Town = t.Town, NameOfDistrict = t.NameOfDistrict, Id = t.Id }).ToList();

            string json = JsonConvert.SerializeObject(list);

            return Content(json);
        }
        [HttpGet("get/hmpl/types")]
        [Authorize]
        public ContentResult GetRealEstateHomePlaceTypes()
        {
            var list = _context.HomePlaces.
                Select(t => new HomePlaceTypeModel() { Id = t.Id, Name = t.NameOfDistrict }).ToList();

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
                    HomePlaceId = model.HomePlaceId,
                    Description = model.description
                    
                };
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
                estate.HomePlaceId = model.HomePlaceId;
                estate.Description = model.description;
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
        public ContentResult FindEstates(string type,[FromBody] string[] values)
        {
            try
            {
                List<RealEstate> temp = new List<RealEstate>();
                temp.AddRange(_context.RealEstates);
                List<RealEstate> res = new List<RealEstate>();
                if (values[0] != string.Empty && values[1] != string.Empty)
                {
                    temp = null;
                    foreach (var item in _context.RealEstates)
                    {
                        if (item.TerritorySize >= double.Parse(values[0]) && item.TerritorySize <= double.Parse(values[1]))
                        {
                            temp.Add(item);
                        }
                    }
                }
                res = temp;
                if (values[2] != string.Empty && values[3] != string.Empty)
                {
                    temp = null;
                    foreach (var item in res)
                    {
                        if (item.TerritorySize >= double.Parse(values[2]) && item.TerritorySize <= double.Parse(values[3]))
                        {
                            temp.Add(item);
                        }
                    }
                }
                res = temp;
                if (values[4] != string.Empty)
                {
                    temp = null;
                    foreach (var item in res)
                    {
                        if (values[4] == "4+" && item.RoomCount >= 4)
                            temp.Add(item);
                        else if (values[4] != "4+" && item.RoomCount >= int.Parse(values[4]))
                            temp.Add(item);

                    }
                }
                res = temp;
                if (values[5] != string.Empty)
                {
                    temp = null;
                    foreach (var item in res)
                    {
                        foreach (var i in _context.RealEstateTypes)
                        {
                            if (values[5] == i.TypeName && item.TypeId == i.Id)
                                temp.Add(item);
                        }
                    }
                }
                res = temp;
                if (values[6] != string.Empty)
                {
                    temp = null;
                    foreach (var item in res)
                    {
                        if (item.HomePlaceOf.Town == values[6])
                        {
                            temp.Add(item);
                        }
                    }
                }
                res = temp;
                if (values[7] != string.Empty)
                {
                    temp = null;
                    foreach (var item in res)
                    {
                        if (item.HomePlaceOf.NameOfDistrict == values[7])
                        {
                            temp.Add(item);
                        }
                    }
                }
               
                var list = temp.
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
                return Content("Eror:" + rx.Message);
            }
        }
    }
}
