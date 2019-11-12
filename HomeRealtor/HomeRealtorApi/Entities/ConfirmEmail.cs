using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_ConfirmationEmails")]
    public class ConfirmEmail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [ForeignKey("UserOf")]
        public string UserId { get; set; }
        public virtual User UserOf { get; set; }
    }
}
