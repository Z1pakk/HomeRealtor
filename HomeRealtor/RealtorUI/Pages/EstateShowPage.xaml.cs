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
    /// Interaction logic for EstateShowPage.xaml
    /// </summary>
    public partial class EstateShowPage : Page
    {
        List<GetListEstateViewModel> _estates = new List<GetListEstateViewModel>();
        List<GetListEstateViewModel> estates_ = new List<GetListEstateViewModel>();
        string _token;
        //List<ComboBoxModel> Towns = new List<ComboBoxModel>();
        public EstateShowPage(string token)
        {
            InitializeComponent();
            _token = token;

        }

        private void lv_Buy_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selectedId = ((GetListEstateViewModel)lv_Buy.SelectedItem).Id;
            RealEstateAboutPage page = new RealEstateAboutPage(selectedId, _token);
            NavigationService.Navigate(page);
        }

        private void lv_Rent_PreviewMouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            int selectedId = ((GetListEstateViewModel)lv_Rent.SelectedItem).Id;
            RealEstateAboutPage page = new RealEstateAboutPage(selectedId, _token);
            NavigationService.Navigate(page);
        }



        public async void cbTown_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
           // cbTown.IsEditable = false;
            if (cbRegion.SelectedItem != null)
            {    
                BaseServices service = new BaseServices();
                    string url = "https://localhost:44325/api/RealEstate/get/districts/bytown/" + ((ComboBoxModel)(cbTown.SelectedItem)).Id;
                    List<DistrictModel> districts = (await service.GetDistricts(url, _token)).Result;
                string er = (await service.GetDistricts(url, _token)).ExceptionMessage;
                url = "https://localhost:44325/api/RealEstate/get/district/types";
                List<DistrictTypeModel> dTypes = (await service.GetDistrictTypes(url, _token)).Result;
                List<ComboBoxModel> temp = new List<ComboBoxModel>();
                for (int i = 0; i < dTypes.Count - 1; i++)
                {
                    temp.Add(new ComboBoxModel { Name = "   " + dTypes[i].NameOfType, Id = 0 });
                    temp.AddRange(districts.Where(t => t.DistrictTypeId == dTypes[i].Id).Select(t => new ComboBoxModel() { Id = t.Id, Name = t.NameOfDistrict }));
                }
                cbDistrict.ItemsSource = temp;
            }
           

        }



        public async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/find/Sell";
            //string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text, cbRCount.Text, ((ComboBoxModel)(cbType.SelectedItem)).Name, ((ComboBoxModel)(cbRegion.SelectedItem)).Name, ((ComboBoxModel)(cbTown.SelectedItem)).Name, ((ComboBoxModel)(cbDistrict.SelectedItem)).Name };
            string reg=string.Empty, town = string.Empty, distr = string.Empty, count = string.Empty,type=string.Empty;
            if (cbRCount.Text != null)
                count = cbRCount.Text;
            if (((ComboBoxModel)(cbType.SelectedItem)).Name != null)
                type = ((ComboBoxModel)(cbRegion.SelectedItem)).Name;
            if (((ComboBoxModel)(cbRegion.SelectedItem)).Name != null)
                reg = ((ComboBoxModel)(cbRegion.SelectedItem)).Name;
            if (((ComboBoxModel)(cbTown.SelectedItem)).Name != null)
                town = cbRCount.Text;
            if (((ComboBoxModel)(cbDistrict.SelectedItem)).Name != null)
                distr = cbRCount.Text;
            string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text, count, type, reg,town, distr};

            List<GetListEstateViewModel> estates = await services.GetFindedEstatesAsync(url, JsonConvert.SerializeObject(arr), "POST", tok);
            lv_Buy.ItemsSource = estates;
        }

        public async void cbRegion_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/towns";
            List<TownModel> res = (await service.GetTowns(url, _token)).Result;
            List<ComboBoxModel> arr = res.Where(t=>t.RegionId==((ComboBoxModel)(cbRegion.SelectedItem)).Id).Select(t => new ComboBoxModel() { Id = t.Id, Name = t.NameOfTown }).ToList();
            //Towns.AddRange(arr.Select(t => _region + ", " + t.Name));
            cbTown.ItemsSource = arr;

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
            List<ComboBoxModel> temp = res.Select(t => new ComboBoxModel() { Id = t.Id, Name = t.Name }).ToList();
            cbType.ItemsSource = temp;

            url = "https://localhost:44325/api/RealEstate/get/regions";
            List<RegionModel> regions = (await service.GetRegions(url, _token)).Result;
            cbRegion.ItemsSource = regions.Select(t => new ComboBoxModel() { Id = t.Id, Name = t.NameOfRegion }).ToList(); 
            cbRCount.ItemsSource = new string[] { "1", "2", "3", "4+" };
            //cbDistrict.IsEditable = false;
            //cbRCount.IsEditable = false;
            //cbTown.IsEditable = false;
            //cbType.IsEditable = false;
        }
    }
}