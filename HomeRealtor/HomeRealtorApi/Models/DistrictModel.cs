using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class DistrictModel
    {
        public int Id { get; set; }
        public string NameOfDistrict { get; set; }
        public int TownId { get; set; }
        public int DistrictTypeId { get; set; }

    }
}
