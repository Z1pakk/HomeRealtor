﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tblOrders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EstateOf")]
        public int ApartId { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("UserOf")]
        public string UserId { get; set; }

        [ForeignKey("RealtorOf")]
        public string RealtorId { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }


        public virtual RealEstate EstateOf { get; set; }
        public virtual User UserOf { get; set; }
        public virtual User RealtorOf { get; set; }
    }
}
