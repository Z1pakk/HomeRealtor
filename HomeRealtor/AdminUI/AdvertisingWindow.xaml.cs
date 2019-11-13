using AdminUI.Models;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace AdminUI
{
    /// <summary>
    /// Interaction logic for AdvertizingWindow.xaml
    /// </summary>
    public partial class AdvertizingWindow : MetroWindow
    {
        public AdvertizingWindow()
        {
            InitializeComponent();
            ShowAdvertising();
        }

        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            AddAdvertisingWindow window = new AddAdvertisingWindow();
            window.ShowDialog();
        }
        
        private void ShowAdvertising()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/user/showadddvertising");
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();

            List<ShowAdvertisingModel> advertisings = new List<ShowAdvertisingModel>();
            using (StreamReader writer = new StreamReader(response.GetResponseStream()))
            {
                string temp = writer.ReadToEnd();
                advertisings = JsonConvert.DeserializeObject<List<ShowAdvertisingModel>>(temp);
            }

            AdvertisngDg.Items.Add(advertisings);
        }
    }
}
