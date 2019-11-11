using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    // Oblast
    [Table("tbl_Regions")]
    public class Region
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(20)]
        public string NameOfRegion { get; set; }
        public virtual ICollection<Town> Towns { get; set; }
    }
}
