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
using System.Collections.ObjectModel;

namespace RealtorUI.Pages
{
    /// <summary>
    /// Interaction logic for RealtorWindow.xaml
    /// </summary>
    /// 
    public partial class AddRealEstatePage : Page
    {
        string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");

        public UserInfoModel UserM { get; set; }

        ObservableCollection<LVImages> lvImages = new ObservableCollection<LVImages>();
        List<string> images = new List<string>();

        List<TypeViewModel> types = new List<TypeViewModel>();
        List<TypeViewModel> sellTypes = new List<TypeViewModel>();
        List<TypeViewModel> homePlace = new List<TypeViewModel>();

        List<string> typesId = new List<string>();
        List<string> sellTypesId = new List<string>();
        List<string> homePlaceId = new List<string>();
        private string imagePath;

        public AddRealEstatePage(UserInfoModel u)
        {
            InitializeComponent();
            this.WindowHeight = 750;
            this.WindowWidth = 800;
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

            httpWebRequest = WebRequest.CreateHttp("https://localhost:44325/api/realEstate/get/hmpl/types");
            webResponse = httpWebRequest.GetResponse();
            using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
            {
                homePlace = JsonConvert.DeserializeObject<List<TypeViewModel>>(reader.ReadToEnd());
            }

            foreach (var type in types)
                typesId.Add(type.Name);
            foreach (var sell in sellTypes)
                sellTypesId.Add(sell.Name);
            foreach (var place in homePlace)
                homePlaceId.Add(place.Name);

            cbType.ItemsSource = typesId;
            cbSellType.ItemsSource = sellTypesId;
            cbHomePlace.ItemsSource = homePlaceId;
            lvPhotos.ItemsSource = lvImages;


        }

        private void BtnAddPhoto_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            var res = openFile.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                imagePath = openFile.FileName;
                
                images.Add(ImageHelper.ImageToBase64(imagePath));
                lvImages.Add(new LVImages() { BitImage = new BitmapImage(new Uri(imagePath))});

            }

        }

        private void BtnAddRealEstate_Click(object sender, RoutedEventArgs e)
        {
            imagePath = images.First();
            RealEstateViewModel realEstate = new RealEstateViewModel()
            {
                Active = true,
                Image = imagePath,
                Location = tbStreet.Text,
                Price = Double.Parse(tbPrice.Text),
                StateName = tbState.Text,
                TerritorySize = Double.Parse(tbArea.Text),
                TypeId = types.FirstOrDefault(t => t.Name == (string)cbType.SelectedItem).Id,
                TimeOfPost = DateTime.Now,
                RoomCount = Int32.Parse(tbRoomCount.Text),
                SellType = sellTypes.FirstOrDefault(t => t.Name == (string)cbSellType.SelectedItem).Id,
                HomePlaceId = homePlace.FirstOrDefault(t => t.Name == (string)cbHomePlace.SelectedItem).Id,
                UserId = UserM.Id,
                images = images,
                description = tbAbout.Text
                
            };

            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/realEstate/add");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(JsonConvert.SerializeObject(realEstate));
            }

            WebResponse webResponse = request.GetResponse();
         
            NavigationService.GoBack();

        }

    }
}
