using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class ShowAdvertisingModel
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Contacts { get; set; }
        public string RealtorName{ get; set; }
    }
}
