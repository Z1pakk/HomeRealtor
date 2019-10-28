using APIConnectService.Models;
using APIConnectService.Service;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
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
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/buy";
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
    }
}