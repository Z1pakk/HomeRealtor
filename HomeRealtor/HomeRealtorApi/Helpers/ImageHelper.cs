using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeRealtorApi.Helpers
{
    public class ImageHelper

    {
        public static Image Base64ToImage(string imgBase64)
        {
            byte[] imgBytes = Convert.FromBase64String(imgBase64);
            using (MemoryStream stream = new MemoryStream(imgBytes,0,imgBytes.Length))
            {
                stream.Position = 0;
                return Bitmap.FromStream(stream);
            }
            
        }
    }
}
