using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Models
{
    public class ListAndCount
    {
        public int EstatesCount { get; set; }
        public List<GetListEstateViewModel> Estates { get; set; }
    }
}
