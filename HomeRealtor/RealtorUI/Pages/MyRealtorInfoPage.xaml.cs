using APIConnectService.Helpers;
using APIConnectService.Service;
using Newtonsoft.Json;
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
    /// Логика взаимодействия для MyRealtorInfoPage.xaml
    /// </summary>
    public partial class MyRealtorInfoPage : Page
    {
        public UserModel UserM { get; set; }
        public MyRealtorInfoPage(UserModel user)
        {
            InitializeComponent();
            UserM = user;
            imgPerson.Source = new BitmapImage(new Uri("https://localhost:55945/Content/" + user.Image));
            lblName.Content = lblName.Content + user.FirstName + " " + user.LastName;
            lblEmail.Content = lblEmail.Content + user.Email;
            lblAge.Content = lblAge.Content + user.Age.ToString();
            lblPhone.Content = lblPhone.Content + user.PhoneNumber;
        }

        private async void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dgEstates.SelectedItem != null)
            {
                BaseServices services = new BaseServices();
                ServiceResult resOrder = await services.OrderMethod("https://localhost:55945/api/order/orders", string.Empty, "GET");
                if (resOrder.Success == true)
                {
                    foreach (var item in resOrder.Result)
                    {
                        if (((OrderModel)(item)).ApartId == ((RealEstateModel)dgEstates.SelectedItem).Id)
                        {
                            ServiceResult res = await services.OrderMethod("https://localhost:55945/api/order/delete/" + ((OrderModel)(item)).Id, string.Empty, "GET");
                            if (res.Success == false)
                                MessageBox.Show(res.ExceptionMessage);
                            break;
                        }
                    }


                }
            }
        }
        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BaseServices services = new BaseServices();
            ServiceResult res = await services.RealEstateMethod("https://localhost:55945/api/realestate/get", string.Empty, "GET");
            if (res.Success == true)
            {
                dgEstates.Items.Clear();
                foreach (var item in res.Result)
                {
                    if (((RealEstateModel)(item)).UserId == UserM.Id.ToString())
                    {
                        dgEstates.Items.Add(item);
                    }
                }
            }
            else MessageBox.Show(res.ExceptionMessage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnAddMyInfo_Click(object sender, RoutedEventArgs e)
        {
            UserModel sser = UserM;
            sser.AboutMe = txtAboutMe.Text;
            BaseServices services = new BaseServices();
            ServiceResult res = await services.UserMethod("https://localhost:55945/api/user/edit/" + UserM.Id, JsonConvert.SerializeObject(sser), "PUT");
            if (res.Result == false)
                MessageBox.Show(res.ExceptionMessage);
            else MessageBox.Show(res.Result);
        }
    }
}
