using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_RealStates")]
    public class RealEstate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, StringLength(20)]
        public string StateName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required,StringLength(500)]
        public string Location { get; set; }

        [Required]
        public int RoomCount { get; set; }

        public DateTime TimeOfPost { get; set; }

        public double TerritorySize { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        public string Coordinates { get; set; }

        [Required]
        public bool Active { get; set; }

        [ForeignKey("TypeOf")]
        public int TypeId { get; set; }

        [ForeignKey("UserOf")]
        public string UserId { get; set; }

        [ForeignKey("SellOf")]
        public int SellType { get; set; }

        [ForeignKey("HomePlaceOf")]
        public int HomePlaceId { get; set; }

        public virtual HomePlace HomePlaceOf { get; set; }
        public virtual RealEstateType TypeOf { get; set; }
        public virtual User UserOf { get; set; }
        public virtual RealEstateSellType SellOf { get; set; }
        public virtual ICollection<ImageEstate> ImageEstates { get; set; }
        public virtual ICollection<Advertising> Advertisings { get; set; }

    }
}
