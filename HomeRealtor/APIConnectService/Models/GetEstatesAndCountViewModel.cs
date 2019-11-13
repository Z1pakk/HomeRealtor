using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Models
{
    public class GetEstatesAndCountViewModel
    {
        public int EstatesCount { get; set; }
        public List<GetListEstateViewModel> Estates { get; set; }
    }
}
