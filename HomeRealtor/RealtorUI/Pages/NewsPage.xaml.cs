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
    /// Interaction logic for NewsPage.xaml
    /// </summary>
    public partial class NewsPage : Page
    {
        int _id;
        
        public NewsPage(int id)
        {

            InitializeComponent();
            _id = id;
            string url = $"https://localhost:44325/api/news/get/{_id}";
            string s;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.Method = "GET";
            myReq.ContentType = "application/json";
            WebResponse response = myReq.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                s = stream.ReadToEnd();
            }
            NewsModels news = JsonConvert.DeserializeObject <NewsModels>(s);

            txtHeadline.Content = news.Headline;
            txtText.Text = news.Text;
            ima
            ;
            


        }
    }
}
