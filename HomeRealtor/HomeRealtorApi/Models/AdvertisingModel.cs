using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class AdvertisingModel
    {
        public string Image { get; set; }
        public string StateName { get; set; }
        public double Price { get; set; }
        public string Contacts { get; set; }
        public string UserId { get; set; }
    }
}
