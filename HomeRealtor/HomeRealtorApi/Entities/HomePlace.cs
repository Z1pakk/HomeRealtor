using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    public class HomePlace
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string Town { get; set; }
        [Required, StringLength(40)]
        public string NameOfDistrict { get; set; }
        [ForeignKey("HomePlaceTypeOf")]
        public int HomePlaceId { get; set; }
        public virtual ICollection<RealEstate> RealEstates { get; set; }
        public virtual HomePlaceType HomePlaceTypeOf { get; set; }
    }
}
