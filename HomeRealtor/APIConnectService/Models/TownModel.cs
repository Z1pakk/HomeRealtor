using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Models
{
    public class TownModel
    {
        public int Id { get; set; }
        public string NameOfTown { get; set; }
        public int RegionId { get; set; }
    }
}
