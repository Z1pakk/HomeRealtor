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
using Newtonsoft.Json;
using RealtorUI.Models;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for RealtorWindow.xaml
    /// </summary>
    public partial class RealtorWindow : Page
    {
        private string imagePath;
        public RealtorWindow()
        {
            InitializeComponent();
        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            List<ImageEstateModel> images = new List<ImageEstateModel>();
            RealEstateModel realEstate = new RealEstateModel() {
                Active = true,
                //Location = 
            };

            OpenFileDialog openFile = new OpenFileDialog();
            var res = openFile.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                imagePath = openFile.FileName;
                lvPhotos.Items.Add(new BitmapImage(new Uri(imagePath)));
            }

            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:55603/api/values/realEstate/add");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
               // writer.Write(JsonConvert.SerializeObject());
            }


        }

    }
}
