using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_HomePlaces")]
    public class HomePlace
    {
        [Key,ForeignKey("DistrictOf")]
        public int DistrictId { get; set; }
        [Key,ForeignKey("RealEstateOf")]
        public int RealEstateId { get; set; }
        public virtual RealEstate RealEstateOf { get; set; }
        public virtual District DistrictOf { get; set; }
    }
}
