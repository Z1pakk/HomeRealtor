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

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Page
    {
        public AboutUs()
        {
            InitializeComponent();
            
           
           
           
            
        }
        public void GetNews()
        {
            string url = @"http://localhost:55164/api/news/news";
            try
            {


                string s;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = myReq.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    s = stream.ReadToEnd();
                }
                response.Close();

                //List<News> valuta = JsonConvert.DeserializeObject<List<News>>(s);



               // foreach (var item in valuta)
               // {
               //     lstAddIns.Items.Add(Name = item.Headline.ToString());
               // }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
