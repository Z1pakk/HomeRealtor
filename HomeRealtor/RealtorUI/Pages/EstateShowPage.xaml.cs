﻿using APIConnectService.Models;
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
        public EstateShowPage(string token)
        {
            InitializeComponent();
            _token = token;

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
            cbTown.IsEditable = false;
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/hmpl";
            List<HomePlaceModel> hm = (await service.GetHomePlaces(url, "GET", _token)).Result;

            url = "https://localhost:44325/api/RealEstate/get/hmpl/types";
            List<HomePlaceTypeModel> hmTypes =(await service.GetHomePlaceTypes(url, "GET", _token)).Result;
            List<string> districts = new List<string>();
            for (int i = 0; i < hmTypes.Count - 1; i++)
            {
                districts.AddRange(hm.Where(t => t.HomePlaceTypeId == hmTypes[i].Id&&t.Town==cbTown.Text).Select(t => t.NameOfDistrict));
            }
            cbDistrict.ItemsSource = districts;
        }

        private void cbDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbDistrict.IsEditable = false;
        }

        private void cbRCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbRCount.IsEditable = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/find/Sell";
            //string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text, cbRCount.Text, cbType.Text, cbTown.Text, cbDistrict.Text };
            string[] arr = { txtAreaFrom.Text, txtAreaTo.Text, txtPriceFrom.Text, txtPriceTo.Text, string.Empty, cbType.Text, string.Empty, string.Empty};
            List<GetListEstateViewModel> estates = services.GetFindedEstates(url,JsonConvert.SerializeObject(arr), "POST", tok);
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
            List<HomePlaceModel> hm = (await service.GetHomePlaces(url, "GET", _token)).Result;
            cbTown.ItemsSource = hm.Select(t => t.Town).Distinct();
            cbRCount.ItemsSource = new string[] { "1", "2", "3", "4+" };
        }
    }
}