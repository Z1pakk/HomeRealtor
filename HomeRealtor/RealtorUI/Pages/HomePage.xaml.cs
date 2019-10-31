using Newtonsoft.Json;
using RealtorUI.Models;
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

namespace RealtorUI.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        public HomePage()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/advertising/advertising");
            request.Method = "GET";
            request.ContentType = "application/json";
            List<AdvertisingModel> advertisings = new List<AdvertisingModel>();

            WebResponse response = request.GetResponse();
            using (StreamReader writer = new StreamReader(response.GetResponseStream()))
            {
                string temp = writer.ReadToEnd();
                advertisings = JsonConvert.DeserializeObject<List<AdvertisingModel>>(temp);
            }


            InitializeComponent();

            lbAdvertising.ItemsSource = advertisings;

        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            //AdvertisingPage home = new AdvertisingPage();
            //this.Content = home;
        }
    }
}
