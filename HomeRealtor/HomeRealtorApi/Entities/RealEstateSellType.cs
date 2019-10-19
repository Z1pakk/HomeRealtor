using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tblSellTypes")]
    public class RealEstateSellType
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(200)]
        public string SellTypeName { get; set; }

        public virtual ICollection<RealEstate> RealEstates { get; set; }
    }
}
