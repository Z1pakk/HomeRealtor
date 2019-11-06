using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    [Table ("tbl.News")]
    public class News
    {
        [Key]public int Id { get; set; }
        [Required] public string Headline { get; set; }
        [Required] public string Text { get; set; }
        public string Image { get; set; }
    }
}
