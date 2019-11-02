using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Entities
{
    public class HomePlaceType
    {
        public int Id { get; set; }
        public string NameOfType { get; set; }
        public virtual ICollection<HomePlace> HomePlaces { get; set; }
    }
}
