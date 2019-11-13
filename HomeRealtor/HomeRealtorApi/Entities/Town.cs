using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Towns")]
    public class Town
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string NameOfTown { get; set; }
        [ForeignKey("RegionOf")]
        public int RegionId { get; set; }
        public virtual Region RegionOf { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
