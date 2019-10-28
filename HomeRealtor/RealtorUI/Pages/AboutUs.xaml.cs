using Newtonsoft.Json;
using RealtorUI.Models;
using RealtorUI.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs :Page
    {
        public AboutUs()
        {
            InitializeComponent();
            GetNews();
        }
        public void GetNews()
        {
            string url = @" https://localhost:44325/api/news/news";
            try
            {


                string s;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                myReq.Method="GET";
                myReq.ContentType = "application/json";
                WebResponse response = myReq.GetResponse();
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    s = stream.ReadToEnd();
                }
                response.Close();

                List<NewsModels> news  = JsonConvert.DeserializeObject<List<NewsModels>>(s);
                
               foreach (var item in news)
                {
                    lstAddIns.Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void lstAddIns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewsModels selectedOffer = (lstAddIns.SelectedItem as NewsModels);
            if (selectedOffer != null)
            {
               int selectedId = ((NewsModels)lstAddIns.SelectedItem).Id;
               NewsPage page = new NewsPage(selectedId);
                NavigationService.Navigate(page);
            }
            
        }
    }
}
