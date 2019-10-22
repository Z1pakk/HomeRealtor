using APIConnectService.Models;
using APIConnectService.Service;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
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

        public RealEstateAboutPage(int id)
        {
            InitializeComponent();
            BaseServices service = new BaseServices();
            _id = id;
            string url = $"http://localhost:58446/api/RealEstate/get/byid/{_id}";
            GetRealEstateViewModel model = service.GetEstate(url, "GET");

            var uri = new Uri(model.Image);
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
        }
    }
}
