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
        List<RealEstateViewModel> Estates = new List<RealEstateViewModel>();
        public EstateShowPage()
        {
            InitializeComponent();
            BaseServices service = new BaseServices();
            string url = "http://localhost:58446/api/RealEstate/get";
            var result = service.GetEstates(url, "GET");
            for (int i = 0; i < result.Count; i++)
            {
                Estates.Add(new RealEstateViewModel()
                {
                    Image = result[i].Image,
                    StateName = result[i].StateName,
                    Price = result[i].Price,
                    Location = result[i].Location,
                    RoomCount = result[i].RoomCount,
                    TerritorySize = result[i].TerritorySize,
                    TimeOfPost = result[i].TimeOfPost,
                    Active = result[i].Active,
                    TypeId = result[i].TypeId,
                    UserId = result[i].UserId,
                    SellType = result[i].SellType
                });
            }
            lv_Buy.ItemsSource = Estates.Where(t => t.SellType == 398);
            lv_Rent.ItemsSource = Estates.Where(t => t.SellType == 397);

        }

        private void TabItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lv_Buy_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RealEstateViewModel item = Estates.FirstOrDefault(t => t.StateName == (((RealEstateViewModel)lv_Buy.SelectedItem).StateName));
            RealEstateAboutPage page = new RealEstateAboutPage(item);
            NavigationService.Navigate(page);
        }
    }
}