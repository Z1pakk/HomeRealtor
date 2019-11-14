using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class GetListEstateViewModel
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
        public int RoomCount { get; set; }
        public double TerritorySize { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
    }
}
