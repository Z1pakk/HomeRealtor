using APIConnectService.Models;
using APIConnectService.Service;
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
    /// Interaction logic for RealEstateAboutPage.xaml
    /// </summary>
    public partial class RealEstateAboutPage : Page
    {
        int _id;
        string _token;
        string _fullName;
        public RealEstateAboutPage(int id, string token)
        {
            InitializeComponent();
            BaseServices service = new BaseServices();
            _id = id;
            _token = token;
            string url = $"https://localhost:44325/api/RealEstate/get/byid/{_id}";
            GetRealEstateViewModel model = service.GetEstate(url, "GET");

            var uri = new Uri(model.Image);
            var bitmap = new BitmapImage(uri);
            img_Estate.Source = bitmap;
            txt_Name.Text += model.StateName;
            txt_Price.Text += model.Price.ToString();
            txt_Location.Text += model.Location;
            txt_RoomCount.Text += model.RoomCount.ToString();
            txt_TerritorySize.Text += model.TerritorySize.ToString();
            txt_TimeOfPost.Text += model.TimeOfPost.ToString();
            if (model.Active == true)
            {
                txt_Active.Text += "On Saling";
            }
            else
            {
                txt_Active.Text += "Sold";
            }
            txt_Type.Text += model.TypeName;
            txt_Owner.Text += model.FullName;
            _fullName = model.FullName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderPage page = new OrderPage(_token, _fullName, _id);
            NavigationService.Navigate(page);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    BaseServices service = new BaseServices();
        //    string url = $"http://localhost:58446/api/RealEstate/get/byid/{_id}";
        //    GetRealEstateViewModel model = service.GetEstate(url, "GET");
        //    AdvertisingModel advModel = new AdvertisingModel()
        //    {
        //        Image = model.Image,
        //        StateName = model.StateName,
        //        Contacts = model.FullName,
        //        Price = model.Price
        //    };

        //    HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44399/api/advertising/add");
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
        //    {
        //        writer.Write(JsonConvert.SerializeObject(advModel));
        //    }

        //    WebResponse response = request.GetResponse();
            
        //}
    }
}
