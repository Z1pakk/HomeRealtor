using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Advertisings")]
    public class Advertising
    {
        [Key] public int Id { get; set; }

        [ForeignKey("UserOf")]
        public string UserId { get; set; }
        public virtual User UserOf { get; set; }

        [ForeignKey("RealEstateOf")]
        public int RealEstsateId { get; set; }
        public virtual RealEstate RealEstateOf { get; set; }
    }
}
