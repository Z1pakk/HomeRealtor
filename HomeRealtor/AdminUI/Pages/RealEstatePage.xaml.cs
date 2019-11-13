using AdminUI.Pages;
using APIConnectService.Models;
using APIConnectService.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<GetListEstateViewModel> estates = new ObservableCollection<GetListEstateViewModel>();
        public RealEstatePage()
        {
            InitializeComponent();

            lv.ItemsSource = estates;

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
            var result = JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].IsDeleted == true)
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        btnBackground = "#FFDE0A0A",
                        btnContext = "Deleted"
                    });

                }
                else if (result[i].Active == true)
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        Active = result[i].Active,
                        btnBackground = "#FF10CF07",
                        btnContext = "Active"
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

        }
        bool IsDeleted = false;
        int id;
        private void lv_Rent_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
           
        }
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request;
            if (IsDeleted)
            {
                request = WebRequest.CreateHttp($"https://localhost:44325/api/RealEstate/restore/{id}");
                request.Method = "GET";
                GetListEstateViewModel model = estates.FirstOrDefault(t => t.Id == id);
                estates.Remove(model);
            }

            else
            {

                request = WebRequest.CreateHttp($"https://localhost:44325/api/RealEstate/del/{id}");
                request.Method = "DELETE";
                GetListEstateViewModel model = estates.FirstOrDefault(t => t.Id == id);
                estates.Remove(model);
            }
            request.ContentType = "application/json";
            WebResponse wr = request.GetResponse();
            string responceFromServer;
            using (Stream streamResponce = wr.GetResponseStream())
            {
                StreamReader reader = new StreamReader(streamResponce);
                responceFromServer = reader.ReadToEnd();
            }
            //MessageBox.Show("All done !");
            //NavigationService.Navigate(page);
        }
        private void Lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv.SelectedItem != null)
            {
                id = ((GetListEstateViewModel)lv.SelectedItem).Id;
                BaseServices service = new BaseServices();

                //string url = $"https://localhost:44325/api/RealEstate/get/byid/{_id}";


                HttpWebRequest request = WebRequest.CreateHttp($"https://localhost:44325/api/RealEstate/get/byid/{id}");
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
                GetRealEstateViewModel model = JsonConvert.DeserializeObject<GetRealEstateViewModel>(responceFromServer);

                if (model.IsDeleted)
                {
                    btnDel.Content = "Restore";
                    btnDel.Background = (Brush)new BrushConverter().ConvertFromString("White");
                    BrushConverter brush = new BrushConverter();

                    btnDel.BorderBrush = (Brush)new BrushConverter().ConvertFromString("#FF17A5FD");

                    IsDeleted = true;
                }
                //#FFDE0A0A
                else
                {
                    btnDel.Content = "Delete";
                    btnDel.Background = (Brush)new BrushConverter().ConvertFromString("#FFDE0A0A");
                    BrushConverter brush = new BrushConverter();



                    IsDeleted = false;
                }
                var uri = new Uri(model.Image);
                var bitmap = new BitmapImage(uri);
                img_Estate.Source = bitmap;
                txt_Name.Text = "Real Estate Title: " + model.StateName;
                txt_Price.Text = "Prcie: " + model.Price.ToString();
                txt_Location.Text = "Location: " + model.Location;
                txt_RoomCount.Text = "Rooms Count: " + model.RoomCount.ToString();
                txt_TerritorySize.Text = "Size: " + model.TerritorySize.ToString();
                txt_TimeOfPost.Text = "Posted: " + model.TimeOfPost.ToString();
                if (model.Active == true)
                {
                    txt_Active.Text = "Is Active: " + "On Saling";
                }
                else
                {
                    txt_Active.Text = "Is Active: " + "Sold";
                }
                txt_Type.Text = "Type of Estate: " + model.TypeName;
                txt_Owner.Text = "Owner/Seller: " + model.FullName;
                sv.Visibility = Visibility.Visible;
                //frame.Navigate(page);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            estates.Clear();
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/RealEstate/getSold");
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
            var result = JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            estates.Clear();
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/RealEstate/getActive");
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
            var result = JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
            {
                estates.Add(new GetListEstateViewModel()
                {
                    Id = result[i].Id,
                    Image = result[i].Image,
                    StateName = result[i].StateName,
                    RoomCount = result[i].RoomCount,
                    TerritorySize = result[i].TerritorySize,
                    Active = result[i].Active,
                    btnBackground = "#FF10CF07",
                    btnContext = "Active"
                });
            }
            
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            estates.Clear();
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
            var result = JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].IsDeleted == true)
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        btnBackground = "#FFDE0A0A",
                        btnContext = "Deleted"
                    });

                }
                else if (result[i].Active == true)
                {
                    estates.Add(new GetListEstateViewModel()
                    {
                        Id = result[i].Id,
                        Image = result[i].Image,
                        StateName = result[i].StateName,
                        RoomCount = result[i].RoomCount,
                        TerritorySize = result[i].TerritorySize,
                        Active = result[i].Active,
                        btnBackground = "#FF10CF07",
                        btnContext = "Active"
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
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            estates.Clear();
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/RealEstate/getDeleted");
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
            var result = JsonConvert.DeserializeObject<List<GetRealEstateViewModel>>(responceFromServer);
            request.ContentType = "application/json";
            for (int i = 0; i < result.Count; i++)
            {
                estates.Add(new GetListEstateViewModel()
                {
                    Id = result[i].Id,
                    Image = result[i].Image,
                    StateName = result[i].StateName,
                    RoomCount = result[i].RoomCount,
                    TerritorySize = result[i].TerritorySize,
                    Active = result[i].Active,
                    btnBackground = "#FFDE0A0A",
                    btnContext = "Deleted"
                });
            }
            lv.ItemsSource = estates;
           
            
        }

        private void Lv_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
