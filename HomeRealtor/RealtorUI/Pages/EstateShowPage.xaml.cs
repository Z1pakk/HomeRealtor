﻿using APIConnectService.Models;
using APIConnectService.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace RealtorUI.Pages
{
    public partial class EstateShowPage : Page
    {
        List<GetListEstateViewModel> _estates = new List<GetListEstateViewModel>();
        List<GetListEstateViewModel> estates_ = new List<GetListEstateViewModel>();
        string _token;
        int currentPageIndex = 0;
        int totalPage = 0;
        int itemsCount = 0;
        ObservableCollection<Button> buttons = new ObservableCollection<Button>();

        public EstateShowPage(string token)
        {
            InitializeComponent();
            _token = token;
            lv_Paging.ItemsSource = buttons;

            CollectionViewSource view = new CollectionViewSource();
            ObservableCollection<GetListEstateViewModel> customers = new ObservableCollection<GetListEstateViewModel>();

            BaseServices service = new BaseServices();
            string url = $"https://localhost:44325/api/RealEstate/get/Sell?page={currentPageIndex}";
            var result = service.GetEstates(url, "GET");
            
            if (result!=null)
            {
                itemsCount = result.EstatesCount;
            }

            if (itemsCount % 3 != 0)
            {
                totalPage = itemsCount / 3 +1;
            }
            else
            {
                totalPage = itemsCount / 3;
            }

            lvPaggingGenerateButtons();

            for (int i = 0; i < result.Estates.Count; i++)
            {
                _estates.Add(new GetListEstateViewModel()
                {
                    Id = result.Estates[i].Id,
                    Image = @"https://localhost:44325/Content/EstateImages/" + result.Estates[i].Image,
                    StateName = result.Estates[i].StateName,
                    RoomCount = result.Estates[i].RoomCount,
                    TerritorySize = result.Estates[i].TerritorySize,
                });
            }
            lv_Buy.ItemsSource = _estates;

            BaseServices service1 = new BaseServices();
            string url1 = "https://localhost:44325/api/RealEstate/get/Rent";
            var result1 = service1.GetEstates(url1, "GET");

            for (int i = 0; i < result1.Estates.Count; i++)
            {
                estates_.Add(new GetListEstateViewModel()
                {
                    Id = result1.Estates[i].Id,
                    Image = result.Estates[i].Image,
                    StateName = @"https://localhost:44325/Content/EstateImages/" + result1.Estates[i].StateName,
                    RoomCount = result1.Estates[i].RoomCount,
                    TerritorySize = result1.Estates[i].TerritorySize,
                });
            }
            lv_Rent.ItemsSource = estates_;
        }

        private void EstateShowPage_Click(object sender, RoutedEventArgs e)
        {
            int buttonchikIndex = int.Parse((string)((Button)(sender)).Content);
            currentPageIndex = buttonchikIndex - 1;
            BaseServices service = new BaseServices();
            string url = $"https://localhost:44325/api/RealEstate/get/Sell?page={currentPageIndex}";
            var result = service.GetEstates(url, "GET");
            lv_Buy.ItemsSource = null;
            _estates.Clear();
            for (int i = 0; i < result.Estates.Count; i++)
            {
                _estates.Add(new GetListEstateViewModel()
                {
                    Id = result.Estates[i].Id,
                    Image = @"https://localhost:44325/Content/EstateImages/" + result.Estates[i].Image,
                    StateName = result.Estates[i].StateName,
                    RoomCount = result.Estates[i].RoomCount,
                    TerritorySize = result.Estates[i].TerritorySize,
                });
            }
            lv_Buy.ItemsSource = _estates;
            lvPaggingGenerateButtons();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lvBuyGoToPage(1);
            currentPageIndex = 0;
            lvPaggingGenerateButtons();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lvBuyGoToPage(totalPage);
            currentPageIndex = totalPage;
            lvPaggingGenerateButtons();
        }
        private void lvBuyGoToPage(int page)
        {
            currentPageIndex = page - 1;
            BaseServices service = new BaseServices();
            string url = $"https://localhost:44325/api/RealEstate/get/Sell?page={currentPageIndex}";
            var result = service.GetEstates(url, "GET");
            lv_Buy.ItemsSource = null;
            _estates.Clear();
            for (int i = 0; i < result.Estates.Count; i++)
            {
                _estates.Add(new GetListEstateViewModel()
                {
                    Id = result.Estates[i].Id,
                    Image = @"https://localhost:44325/Content/EstateImages/" + result.Estates[i].Image,
                    StateName = result.Estates[i].StateName,
                    RoomCount = result.Estates[i].RoomCount,
                    TerritorySize = result.Estates[i].TerritorySize,
                });
            }
            lv_Buy.ItemsSource = _estates;
        }

        private void lvPaggingGenerateButtons()
        {
            buttons.Clear();
            int max = currentPageIndex < 3 ? 3 : currentPageIndex;
            for(int i = currentPageIndex - 2;i<=max+4;i++)
            {
                if(i<=0)
                {
                    continue;
                }
                else if(i>totalPage)
                {
                    break;
                }
                Button button = new Button();
                button.Content = (i).ToString();
                button.FontSize = 15;
                button.Height = 30;
                button.Width = 60;
                button.Click += EstateShowPage_Click;
                buttons.Add(button);
            }
        }
    }
}