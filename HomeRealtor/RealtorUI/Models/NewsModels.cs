using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtorUI.Models
{
    public class NewsModels
    {
       public int Id { get; set; }
       public string Headline { get; set; }
       public string Text { get; set; }
        public Uri Image { get;  set; }
    }
}
