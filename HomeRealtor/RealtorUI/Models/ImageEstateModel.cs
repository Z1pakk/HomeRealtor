using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RealtorUI.Models
{
    public class ImageEstateModel
    {
        public string Name { get; set; }
        public int EstateId { get; set; }

        public BitmapImage Image { get; set; }
    }
}
