﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public DateTime TimeOfPost { get; set; }

        public double TerritorySize { get; set; }

        [Required]
        public bool Active { get; set; }

        [ForeignKey("TypeOf")]
        public int TypeId { get; set; }

        [ForeignKey("UserOf")]
        public string UserId { get; set; }

        public virtual RealEstateType TypeOf { get; set; }
        public virtual User UserOf { get; set; }
        public virtual ICollection<ImageEstate> ImageEstates { get; set; }

    }
}
