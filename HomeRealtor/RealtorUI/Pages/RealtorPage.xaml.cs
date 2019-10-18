using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RealtorUI.Models;
using Newtonsoft.Json;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for RealtorWindow.xaml
    /// </summary>
    /// 
    public partial class RealtorWindow : Page
    {
        List<ImageEstateModel> images = new List<ImageEstateModel>();
        private string imagePath;

        public RealtorWindow()
        {
            InitializeComponent();
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            var res = openFile.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                imagePath = openFile.FileName;
                lvPhotos.Items.Add(new BitmapImage(new Uri(imagePath)));
                images.Add(new ImageEstateModel() { EstateId = 0, Name = imagePath });
            }

        }

        private void BtnAddRealEstate_Click(object sender, RoutedEventArgs e)
        {
            imagePath = images.First().Name;
            RealEstateModel realEstate = new RealEstateModel()
            {
                Active = true,
                Image = imagePath,
                Location = tbStreet.Text,
                Price = Double.Parse(tbPrice.Text),
                StateName = tbState.Text,
                TerritorySize = Double.Parse(tbArea.Text),
                TypeId = cbType.SelectedIndex,
                TimeOfPost = DateTime.Now
            };

            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:55603/api/values/realEstate/add");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(realEstate));
            }

            request = WebRequest.CreateHttp("http://localhost:55603/api/values/realEstate/getlastid");
            request.Method = "GET";
            request.ContentType = "application/json";

            using (StreamReader reader = new StreamReader(request.GetRequestStream()))
            {
                var RealEstateId = JsonConvert.DeserializeObject<int>(reader.ReadToEnd());
                foreach (var img in images)
                    img.EstateId = RealEstateId;
            }

            request = WebRequest.CreateHttp("http://localhost:55603/api/values/imageEstate/add");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                foreach (var imgEst in images)
                    writer.Write(JsonConvert.SerializeObject(imgEst));
            }
        }
    }
}
