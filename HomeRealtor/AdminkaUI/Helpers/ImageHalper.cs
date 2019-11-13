using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminkaUI.Helpers
{
   public class ImageHalper
    {
        public static string ImageToBase64(string pathToImage)
        {
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(pathToImage))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, image.RawFormat);
                    byte[] imageBytes = stream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}
