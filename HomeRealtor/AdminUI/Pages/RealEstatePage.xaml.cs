using AdminUI.Pages;
using APIConnectService.Models;
using APIConnectService.Service;
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

namespace AdminUI
{
    /// <summary>
    /// Логика взаимодействия для RealEstatePage.xaml
    /// </summary>
    public partial class RealEstatePage : Page
    {
        List<GetListEstateViewModel> estates = new List<GetListEstateViewModel>();
        public RealEstatePage()
        {
            InitializeComponent();
            
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/RealEstate/get");
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse wr = request.GetResponse();
            string responceFromServer;
            using (Stream streamResponce = wr.GetResponseStream())
            {
                StreamReader reader = new StreamReader(streamResponce);
                responceFromServer = reader.ReadToEnd();
            }
            wr.Close();
            var result= JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
            {
                if(result[i].Active==true)
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        Active=result[i].Active,
                        btnBackground= "#FF10CF07",
                        btnContext="Active"
                    });
                    
                }
                else
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        Active = result[i].Active,
                        btnBackground = "#FFFBAD07",
                        btnContext = "Disabled"
                    });
                }
            }
            
            lv.ItemsSource = estates;
        }

        private void lv_Rent_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            int selectedId = ((GetListEstateViewModel)lv.SelectedItem).Id;
            RealEstatePageAbout page = new RealEstatePageAbout(selectedId);
            frame.Navigate(page);
        }

        private void Lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            lv.ItemsSource = estates.Where(t=>t.Active==false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lv.ItemsSource = estates.Where(t => t.Active == true);
        }
    }
}
