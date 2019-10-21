using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Models
{
    public class GetListEstateViewModel
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public string Image { get; set; }
        public int RoomCount { get; set; }
        public double TerritorySize { get; set; }

    }
}
