using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using Microsoft.Maps.MapControl.WPF;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        string _coordinates;
        public RealEstateAboutPage(int id, string token)
        {
            InitializeComponent();
            BaseServices service = new BaseServices();
            _id = id;
            _token = token;
            string url = $"https://localhost:44325/api/RealEstate/get/byid/{_id}";
            GetRealEstateViewModel model = service.GetEstate(url, "GET");
            List<APIConnectService.Models.ImageEstateModel> imgs = new List<APIConnectService.Models.ImageEstateModel>();
            foreach (var item in model.Images)
            {
                imgs.Add(new APIConnectService.Models.ImageEstateModel()
                {
                    SmallImage = @"https://localhost:44325/Content/EstateImages/" + item.SmallImage,
                    MediumImage = @"https://localhost:44325/Content/EstateImages/" + item.MediumImage,
                    LargeImage = @"https://localhost:44325/Content/EstateImages/" + item.LargeImage
                });
            }
            Lv_PhotosMiddle.ItemsSource = imgs;

            var uri = new Uri(@"https://localhost:44325/Content/EstateImages/" + model.Image);
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
            txt_Description.Text = model.Description;
            txt_PhoneNumber.Text += model.PhoneNumber; 
            _coordinates = model.Coordinates;

            if(_coordinates!=null)
            {
                var coor = _coordinates.Split(',');
                List<double> coors = new List<double>();
                foreach (var item in coor)
                {
                    var old = item.Replace('.', ',');

                    double number = double.Parse(old);
                    coors.Add(number);
                }

                if (model.Coordinates != null)
                {
                    myMap.Center = new Location(coors[0], coors[1]);
                    Pushpin pushpin = new Pushpin();
                    pushpin.Location = new Location(coors[0], coors[1]);
                    pushpin.Background = new SolidColorBrush(Colors.CornflowerBlue);
                    mapLayer.AddChild(pushpin, pushpin.Location);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderPage page = new OrderPage(_token, _fullName, _id);
            NavigationService.Navigate(page);
        }

        private void Lv_PhotosMiddle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (APIConnectService.Models.ImageEstateModel)Lv_PhotosMiddle.SelectedItem;
            img_Estate.Source = null;
            Uri uri = new Uri(selected.MediumImage);
            img_Estate.Source = new BitmapImage(uri);
        }

        private void img_Estate_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string path = img_Estate.Source.ToString();
            ImageWindow window = new ImageWindow(path);
            window.ShowDialog ();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel += 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myMap.ZoomLevel -= 1;
        }
    }
}
