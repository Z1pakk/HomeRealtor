using AdminkaUI.Helpers;
using AdminkaUI.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
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

namespace AdminkaUI.Pages
{
    /// <summary>
    /// Interaction logic for AddAdvertisingPage.xaml
    /// </summary>
    public partial class AddAdvertisingPage : Page
    {
        List<ImageModel> images = new List<ImageModel>();
        private string imagePath;

        public AddAdvertisingPage()
        {
            InitializeComponent();
        }

        private void AddPhotosbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            var res = openFile.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                imagePath = openFile.FileName;
                lvPhotos.Items.Add(new BitmapImage(new Uri(imagePath)));
                images.Add(new ImageModel() { EstateId = 0, Name = ImageHalper.ImageToBase64(imagePath) });
            }
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            imagePath = images.First().Name;
            AddAdvertisingModel advertising = new AddAdvertisingModel()
            {
                Image = imagePath,
                Price = Double.Parse(Pricetb.Text),
                StateName = StateNametb.Text,
                Contacs = Contacstb.Text,
                images = images
            };

            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/advertising/add");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(advertising));
            }

            WebResponse webResponse = request.GetResponse();

            NavigationService.GoBack();
        }
    }
}
