using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table("tbl_Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required,StringLength(20),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [StringLength(100)]
        public string  AboutMe { get; set; }
        public string Image { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<ImageUser> ImageUsers { get; set; }
        public virtual ICollection<Advertising> Advertisings { get; set; }

        public virtual List<RealEstate> RealEstates { get; set; }
    }
}
