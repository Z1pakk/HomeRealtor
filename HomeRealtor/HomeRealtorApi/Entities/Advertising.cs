using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Advertisings")]
    public class Advertising
    {
        [Key] public int Id { get; set; }
        [Required] public string Image { get; set; }
        [Required] public string StateName { get; set; }
        [Required] public double Price { get; set; }
        [Required] public string Contacts { get; set; }

        [ForeignKey("UserOf")]
        public int UserId { get; set; }
        public virtual User UserOf { get; set; }
    }
}
