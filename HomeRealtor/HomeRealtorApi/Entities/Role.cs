using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Roles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<UserRole>  UserRoles { get; set; }
    }
}
