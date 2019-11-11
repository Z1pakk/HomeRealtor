using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Districts")]
    public class District
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string NameOfDistrict { get; set; }
        [ForeignKey("TownOf")]
        public int TownId { get; set; }
        [ForeignKey("DistrictTypeOf")]
        public int DistrictTypeId { get; set; }
        public virtual Town TownOf { get; set; }
        public virtual DistrictType DistrictTypeOf { get; set; }
        public virtual ICollection<HomePlace> HomePlaces { get; set; }
    }
}
