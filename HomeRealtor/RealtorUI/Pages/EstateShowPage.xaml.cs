using APIConnectService.Models;
using APIConnectService.Service;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
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
    /// Interaction logic for EstateShowPage.xaml
    /// </summary>
    public partial class EstateShowPage : Page
    {
        List<GetListEstateViewModel> _estates = new List<GetListEstateViewModel>();
        List<GetListEstateViewModel> estates_ = new List<GetListEstateViewModel>();
        private readonly IHostingEnvironment _appEnvironment;
        string _token,_region="";
        public EstateShowPage(string token)
        {
            InitializeComponent();
            _token = token;
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/Sell";
            var result = service.GetEstates(url, "GET");
            for (int i = 0; i < result.Count; i++)
            {
                _estates.Add(new GetListEstateViewModel()
                {
                    Id = result[i].Id,
                    Image = result[i].Image,
                    StateName = result[i].StateName,
                    RoomCount = result[i].RoomCount,
                    TerritorySize = result[i].TerritorySize,
                });
            }
            lv_Buy.ItemsSource = _estates;
            BaseServices service1 = new BaseServices();
            string url1 = "https://localhost:44325/api/RealEstate/get/Rent";
            var result1 = service1.GetEstates(url1, "GET");
            for (int i = 0; i < result1.Count; i++)
            {
                estates_.Add(new GetListEstateViewModel()
                {
                    Id = result1[i].Id,
                    Image = result[i].Image,
                    StateName = result1[i].StateName,
                    RoomCount = result1[i].RoomCount,
                    TerritorySize = result1[i].TerritorySize,
                });
            }
            lv_Rent.ItemsSource = estates_;
            ///////////////////////////////////////////////////////////////////////////////////
            service = new BaseServices();
            url = "https://localhost:44325/api/RealEstate/get/types";
            List<TypeViewModel> res = service.GetEstateTypes(url, "GET",_token);
            cbType.ItemsSource = res.Select(t => t.Name);
            service = new BaseServices();
            url = "https://localhost:44325/api/RealEstate/get/hmpl";
            //List<HomePlaceModel> hm =service.GetHomePlaces(url, "GET", _token).Result.Result;
            //cbTown.ItemsSource = hm.Select(t => t.RealEstateId);
            cbRCount.ItemsSource = new string[] { "1", "2", "3", "4+" };
        }

        private void lv_Buy_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedId=((GetListEstateViewModel)lv_Buy.SelectedItem).Id;
            RealEstateAboutPage page = new RealEstateAboutPage(selectedId,_token);
            NavigationService.Navigate(page);
        }

        private void lv_Rent_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            int selectedId = ((GetListEstateViewModel)lv_Rent.SelectedItem).Id;
            RealEstateAboutPage page = new RealEstateAboutPage(selectedId,_token);
            NavigationService.Navigate(page);
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbType.IsEditable = false;
        }

        private async void cbTown_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            foreach(string item in cbType.Items)
            if (_region != "")
            {
                cbTown.IsEditable = false;
                BaseServices service = new BaseServices();
                string url = "https://localhost:44325/api/RealEstate/get/districts";
                List<DistrictModel> districts = (await service.GetDistricts(url, _token)).Result;

                url = "https://localhost:44325/api/RealEstate/get/district/types";
                List<DistrictTypeModel> dTypes = (await service.GetDistrictTypes(url, _token)).Result;
                List<string> distr = new List<string>();
                for (int i = 0; i < dTypes.Count - 1; i++)
                {
                    distr.AddRange(districts.Where(t => t.DistrictTypeId == dTypes[i].Id && t.TownId.ToString() == cbTown.Text).Select(t => t.NameOfDistrict));
                        distr.Add(@"\t"+dTypes[i].Name);
                }
                cbDistrict.ItemsSource = distr;
                    _region = "";
            }
            else
            {
                BaseServices service = new BaseServices();
                _region = cbTown.Text;
                string url = "https://localhost:44325/api/RealEstate/get/hmpl";
                List<TownModel> hm = (await service.GetTowns(url, _token)).Result;
                List<string> arr = new List<string>();
                    arr.AddRange(hm.Select(t => t.NameOfTown).Distinct());
                cbTown.ItemsSource = arr.Select(t=> _region + ", " + t);
            }
            
        }

        private void cbDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDistrict.IsEditable = false;
        }

        private void cbRCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbRCount.IsEditable = false;
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/find/Sell";
            //string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text, cbRCount.Text, cbType.Text, cbTown.Text, cbDistrict.Text };
            string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text,_region, string.Empty,(cbType.Text).Split(' ')[1], string.Empty, string.Empty};
            List<GetListEstateViewModel> estates = await services.GetFindedEstatesAsync(url,JsonConvert.SerializeObject(arr), "POST", tok);
            lv_Buy.ItemsSource = estates;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/Sell";
            var result = service.GetEstates(url, "GET");
            for (int i = 0; i < result.Count; i++)
            {
                _estates.Add(new GetListEstateViewModel()
                {
                    Id = result[i].Id,
                    Image = result[i].Image,
                    StateName = result[i].StateName,
                    RoomCount = result[i].RoomCount,
                    TerritorySize = result[i].TerritorySize,
                });
            }
            lv_Buy.ItemsSource = _estates;
            BaseServices service1 = new BaseServices();
            string url1 = "https://localhost:44325/api/RealEstate/get/Rent";
            var result1 = service1.GetEstates(url1, "GET");
            for (int i = 0; i < result1.Count; i++)
            {
                estates_.Add(new GetListEstateViewModel()
                {
                    Id = result1[i].Id,
                    Image = result1[i].Image,
                    StateName = result1[i].StateName,
                    RoomCount = result1[i].RoomCount,
                    TerritorySize = result1[i].TerritorySize,
                });
            }
            lv_Rent.ItemsSource = estates_;
            ///////////////////////////////////////////////////////////////////////////////////
            url = "https://localhost:44325/api/RealEstate/get/types";
            List<TypeViewModel> res = service.GetEstateTypes(url, "GET", _token);
            cbType.ItemsSource = res.Select(t => t.Name);

            url = "https://localhost:44325/api/RealEstate/get/hmpl";
            List<RegionModel> reg = (await service.GetRegions(url, _token)).Result;
            cbTown.ItemsSource = reg.Select(t => t.NameOfRegion);
            cbRCount.ItemsSource = new string[] { "1", "2", "3", "4+" };
        }
    }
}