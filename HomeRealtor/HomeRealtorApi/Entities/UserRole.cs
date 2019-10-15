using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_UserRoles")]
    public class UserRole
    {

        
        [Key,ForeignKey("UserOf")]
        public int UserId { get; set; }
        [Key,ForeignKey("RoleOf")]
        public int RoleId { get; set; }

        public virtual User UserOf { get; set; }
        public virtual Role RoleOf { get; set; }

    }
}
