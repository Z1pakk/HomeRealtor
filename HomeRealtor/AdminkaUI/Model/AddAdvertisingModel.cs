using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminkaUI.Model
{
    public class AddAdvertisingModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string StateName { get; set; }
        public double Price { get; set; }
        public string Contacs { get; set; }
        public List<ImageModel> images { get; set; }
    }
}
