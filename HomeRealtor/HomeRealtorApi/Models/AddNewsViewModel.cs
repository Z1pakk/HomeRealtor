using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class AddNewsViewModel
    {
         public string Headline { get; set; }
         public string Text { get; set; }
        public string Image { get; internal set; }
    }
}
