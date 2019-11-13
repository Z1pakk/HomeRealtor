using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_DistrictTypes")]
    public class DistrictType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20),Required]
        public string NameOfType { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
