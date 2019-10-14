using System;
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
        public string StateName { get; set; }
        [ForeignKey("TypeOf")]
        public int TypeId { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime TimeOfPost { get; set; }
        [ForeignKey("UserOf")]
        public int UserId { get; set; }
        public virtual User UserOf { get; set; }
    }
}
