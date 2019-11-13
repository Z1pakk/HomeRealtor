using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class ShowAdvertisingModel
    {
        public string UserName { get; set; }
        public string AdvertisingName { get; set; }
        public DateTime DayEnd { get; set; }
    }
}
