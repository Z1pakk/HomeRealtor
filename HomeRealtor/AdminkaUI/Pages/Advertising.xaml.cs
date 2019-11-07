using AdminkaUI.Model;
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
    /// Interaction logic for Advertising.xaml
    /// </summary>
    public partial class Advertising : Page
    {
        public Advertising()
        {
            InitializeComponent();
            ShowAdvertising();
        }
        private void ShowAdvertising()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/advertising/showadddvertising");
            request.Method = "GET";
            request.ContentType = "application/json";
            List<ShowAdvertisingModel> advertisings = new List<ShowAdvertisingModel>();

            WebResponse response = request.GetResponse();
            using (StreamReader writer = new StreamReader(response.GetResponseStream()))
            {
                string temp = writer.ReadToEnd();
                advertisings = JsonConvert.DeserializeObject<List<ShowAdvertisingModel>>(temp);
            }
             AdvertizingDg.ItemsSource = advertisings;
        }
    }
}
