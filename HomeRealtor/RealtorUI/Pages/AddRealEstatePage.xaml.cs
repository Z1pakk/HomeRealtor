using Microsoft.Win32;
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
using RealtorUI.Models;
using Newtonsoft.Json;
using APIConnectService.Models;

namespace RealtorUI.Pages
{
    /// <summary>
    /// Interaction logic for RealtorWindow.xaml
    /// </summary>
    /// 
    public partial class AddRealEstatePage : Page
    {
        public UserInfoModel UserM { get; set; }
        List<ImageEstateModel> images = new List<ImageEstateModel>();
        List<TypeViewModel> types = new List<TypeViewModel>();
        List<TypeViewModel> sellTypes = new List<TypeViewModel>();

        List<string> typesId = new List<string>();
        List<string> sellTypesId = new List<string>();
        private string imagePath;

        public AddRealEstatePage(UserInfoModel u)
        {
            InitializeComponent();
            UserM = u;
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp("https://localhost:44325/api/realEstate/get/types");
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            WebResponse webResponse = httpWebRequest.GetResponse();
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                types = JsonConvert.DeserializeObject<List<TypeViewModel>>(reader.ReadToEnd());
            }

            httpWebRequest = WebRequest.CreateHttp("https://localhost:44325/api/realEstate/get/selltypes");
            webResponse = httpWebRequest.GetResponse();
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                sellTypes = JsonConvert.DeserializeObject<List<TypeViewModel>>(reader.ReadToEnd());
            }
            foreach (var type in types)
                typesId.Add(type.Name);
            foreach (var sell in sellTypes)
                sellTypesId.Add(sell.Name);

            cbType.ItemsSource = typesId;
            cbSellType.ItemsSource = sellTypesId;

        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            var res = openFile.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                imagePath = openFile.FileName;
                lvPhotos.Items.Add(new BitmapImage(new Uri(imagePath)));
                //images.Add(new ImageEstateModel() { EstateId = 0, Name = ImageHelper.ImageToBase64(imagePath) });
            }

        }

        private void BtnAddRealEstate_Click(object sender, RoutedEventArgs e)
        {
            //imagePath = images.First().Name;
            RealEstateViewModel realEstate = new RealEstateViewModel()
            {
                Active = true,
                Image = imagePath,
                Location = tbStreet.Text,
                Price = Double.Parse(tbPrice.Text),
                StateName = tbState.Text,
                TerritorySize = Double.Parse(tbArea.Text),
                TypeId = types.FirstOrDefault(t=>t.Name== (string)cbType.SelectedItem).Id,
                TimeOfPost = DateTime.Now,
                RoomCount = Int32.Parse(tbRoomCount.Text),
                SellType = sellTypes.FirstOrDefault(t => t.Name == (string)cbType.SelectedItem).Id,
                UserId = UserM.Id,
                //images = images
            };

            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:44325/api/values/realEstate/add");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(realEstate));
            }
            
            NavigationService.GoBack();

        }
    }
}
