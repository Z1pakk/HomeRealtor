using HomeRealtorApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class RealEstateViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string StateName { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public int RoomCount { get; set; }
        public double TerritorySize { get; set; }
        public DateTime TimeOfPost { get; set; }
        public bool Active { get; set; }
        public int TypeId { get; set; }
        public string UserId { get; set; }
        public int HomePlaceId { get; set; }
        public int SellType { get; set; }
        public List<ImageEstateModel> images { get; set; }

    }
}
