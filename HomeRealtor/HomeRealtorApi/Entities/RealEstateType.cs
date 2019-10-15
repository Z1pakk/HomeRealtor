using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tblEstateTypes")]
    public class RealEstateType
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string TypeName { get; set; }

        public virtual List<RealEstate> RealEstates { get; set; }
    }
}
