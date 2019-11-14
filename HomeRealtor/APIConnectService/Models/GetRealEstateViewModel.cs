using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIConnectService.Models
{
    public class GetRealEstateViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string StateName { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public int RoomCount { get; set; }
        public string UserId { get; set; }
        public double TerritorySize { get; set; }
        public DateTime TimeOfPost { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public string TypeName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public string PhoneNumber { get; set; }
        public List<ImageEstateModel> Images { get; set; }
    }
}
