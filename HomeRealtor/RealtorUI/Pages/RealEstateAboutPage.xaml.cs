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
        RealEstateViewModel _model; 
        
        public RealEstateAboutPage(RealEstateViewModel model)
        {
            InitializeComponent();
            _model = model;
            var uri = new Uri(_model.Image);
            var bitmap = new BitmapImage(uri);
            img_Estate.Source = bitmap;
            txt_Name.Text += _model.StateName;
            txt_Price.Text += _model.Price.ToString();
            txt_Location.Text += _model.Location;
            txt_RoomCount.Text += _model.RoomCount.ToString();
            txt_TerritorySize.Text += _model.TerritorySize.ToString();
            txt_TimeOfPost.Text += _model.TimeOfPost.ToString();
            if(_model.Active==true)
            {
                txt_Active.Text += "On Saling";
            }
            else
            {
                txt_Active.Text += "Sold";
            }
            txt_Type.Text += "Budinok";
        } 
    }
}
