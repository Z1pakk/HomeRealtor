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

namespace AdminUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для RealEstatePageAbout.xaml
    /// </summary>
    public partial class RealEstatePageAbout : Page
    {
        int _id;
        string _fullName;
        public RealEstatePageAbout(int id)
        {
            InitializeComponent();
            BaseServices service = new BaseServices();
            _id = id;
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
            GetRealEstateViewModel model= JsonConvert.DeserializeObject<GetRealEstateViewModel>(responceFromServer);


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
    }
}
