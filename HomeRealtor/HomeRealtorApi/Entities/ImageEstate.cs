using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_ImageEstates")]
    public class ImageEstate
    { 
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [ForeignKey("EstateOf")]
            public int EstateId { get; set; }
            public virtual RealEstate EstateOf { get; set; }

        
    }
}
