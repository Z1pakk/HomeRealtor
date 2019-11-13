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
using APIConnectService.Service;
using APIConnectService.Helpers;

namespace RealtorUI.Pages
{
    /// <summary>
    /// Interaction logic for RealtorWindow.xaml
    /// </summary>
    /// 
    public partial class AddRealEstatePage : Page
    {
        string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
        BaseServices services = new BaseServices();
        public UserInfoModel UserM { get; set; }

        ObservableCollection<LVImages> lvImages = new ObservableCollection<LVImages>();
        List<string> images = new List<string>();

        List<TypeViewModel> types = new List<TypeViewModel>();
        List<TypeViewModel> sellTypes = new List<TypeViewModel>();
        List<RegionModel> regions = new List<RegionModel>();
        List<TownModel> towns = new List<TownModel>();
        List<DistrictModel> districts = new List<DistrictModel>();
        List<DistrictTypeModel> districtTypes = new List<DistrictTypeModel>();

        List<string> typesId = new List<string>();
        List<string> sellTypesId = new List<string>();
        List<string> regionId = new List<string>();
        List<string> townId = new List<string>();
        List<string> districtId = new List<string>();
        List<string> districtTypeId = new List<string>();
        private string imagePath;

        public AddRealEstatePage(UserInfoModel u)
        {
            InitializeComponent();
            this.WindowHeight = 750;
            this.WindowWidth = 800;
            UserM = u;
            cbTowns.IsEnabled = false;
            cbDistrictTypes.IsEnabled = false;
            cbDistricts.IsEnabled = false;
            setLists();
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
                //HomePlaceId = homePlace.FirstOrDefault(t => t.Name == (string)cbHomePlace.SelectedItem).Id,
                UserId = UserM.Id,
                images = images,
                description = tbAbout.Text,
                
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
        private async void setLists()
        {
            ServiceResult res ;
            List<TypeViewModel> list =  services.GetEstateTypes("https://localhost:44325/api/realEstate/get/types", "GET", tok);
            if (list.Any())
            {
                foreach (var item in list)
                    types.Add(item);
            }
            list = services.GetEstateTypes("https://localhost:44325/api/realEstate/get/selltypes", "GET", tok);
            if (list.Any())
            {
                foreach (var item in list)
                    sellTypes.Add(item);
            }


            res = await services.GetRegions("https://localhost:44325/api/realEstate/get/regions", tok);
            if (res.Success == true)
            {
                foreach (var item in res.Result)
                    regions.Add(item);
            }


            res = await services.GetTowns("https://localhost:44325/api/realEstate/get/towns", tok);
            if (res.Success == true)
            {
                foreach (var item in res.Result)
                    towns.Add(item);
            }

            res = await services.GetDistrictTypes("https://localhost:44325/api/realEstate/get/district/types", tok);
            if (res.Success == true)
            {
                foreach (var item in res.Result)
                    districtTypes.Add(item);
            }
            res = await services.GetDistricts("https://localhost:44325/api/realEstate/get/districts", tok);
            if (res.Success == true)
            {
                foreach (var item in res.Result)
                    districts.Add(item);
            }

            foreach (var type in types)
                typesId.Add(type.Name);
            foreach (var sell in sellTypes)
                sellTypesId.Add(sell.Name);
            foreach (var region in regions)
                regionId.Add(region.NameOfRegion);
            foreach (var town in towns)
                townId.Add(town.NameOfTown);
            foreach (var districtType in districtTypes)
                districtTypeId.Add(districtType.Name);
            foreach (var district in districts)
                districtId.Add(district.NameOfDistrict);

            cbType.ItemsSource = typesId;
            cbSellType.ItemsSource = sellTypesId;
            cbRegions.ItemsSource = regionId;
            cbTowns.ItemsSource = townId;
            cbDistrictTypes.ItemsSource = districtTypeId;
            lvPhotos.ItemsSource = lvImages;
        }

        private void CbRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbTowns.ItemsSource = null;
            List<TownModel> list = new List<TownModel>();
            cbTowns.IsEnabled = true;
            int id = regions.FirstOrDefault(t => t.NameOfRegion == (string)cbRegions.SelectedItem).Id;
            list = towns.Where(t => t.RegionId == id).ToList();
            townId.Clear();
            foreach (var item in list)
                townId.Add(item.NameOfTown);
            cbTowns.ItemsSource = townId;
        }

        private void CbTowns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDistrictTypes.IsEnabled = true;
        }

        private void CbDistricts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CbDistrictTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<DistrictModel> list = new List<DistrictModel>();
            cbDistricts.IsEnabled = true;
            int idTown = towns.FirstOrDefault(t => t.NameOfTown == (string)cbTowns.SelectedItem).Id;
            int idDistrictType = districtTypes.FirstOrDefault(t => t.Name == (string)cbDistrictTypes.SelectedItem).Id;

            list = districts.Where(t => t.TownId == idTown).Where(t => t.DistrictTypeId == idDistrictType).ToList();
            districtId.Clear();
            foreach (var item in list)
                districtId.Add(item.NameOfDistrict);
            cbDistricts.ItemsSource = districtId;
        }
    }
}
