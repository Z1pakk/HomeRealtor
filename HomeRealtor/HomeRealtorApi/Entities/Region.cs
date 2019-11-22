using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string NameOfRegion { get; set; }
        public virtual ICollection<HomePlace> HomePlaces { get; set; }
    }
}
