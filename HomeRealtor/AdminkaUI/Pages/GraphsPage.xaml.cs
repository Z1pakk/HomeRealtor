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
    /// Логика взаимодействия для GraphsPage.xaml
    /// </summary>
    public partial class GraphsPage : Page
    {
        public List<Stat> OverallStats { get; set; }
        public List<UserCountModel> UsersCount { get; set; }
        public GraphsPage()
        {
            InitializeComponent();
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/RealEstate/getCount");
            request.Method = "GET";
            request.ContentType = "application/json";
            using (WebResponse wr = request.GetResponse())
            {


                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                var s = responceFromServer.Split(',');
                OverallStats = new List<Stat>
            {
                new Stat {Name = "Sell", Count = int.Parse(s[0])},
                new Stat {Name = "Rent", Count = int.Parse(s[1])}
            };
            }
            cs.ItemsSource = OverallStats;



            Count();
            
        }
        public void Count()
        {
           
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/user/getCount");
            request.Method = "GET";
            request.ContentType = "application/json";
            using (WebResponse wr = request.GetResponse())
            {


                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<UserCountModel> l = JsonConvert.DeserializeObject<List<UserCountModel>>(responceFromServer);
                UsersCount = l;
               
            }



            uCs.ItemsSource = UsersCount;
        }
       public class Stat
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}
