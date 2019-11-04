using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using Microsoft.Win32;
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
    /// Логика взаимодействия для MyRealtorInfoPage.xaml
    /// </summary>
    public partial class MyRealtorInfoPage : Page
    {
        public UserInfoModel UserM { get; set; }
        public MyRealtorInfoPage(UserInfoModel user)
        {
            InitializeComponent();
            UserM = user;
            imgPerson.Source = new BitmapImage(new Uri("https://localhost:44325/Content/" + user.Image));
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
                ServiceResult resOrder = await services.OrderMethod("https://localhost:44325/api/order/orders", string.Empty, "GET");
                if (resOrder.Success == true)
                {
                    foreach (var item in resOrder.Result)
                    {
                        if (((OrderModel)(item)).ApartId == ((RealEstateModel)dgEstates.SelectedItem).Id)
                        {
                            ServiceResult res = await services.OrderMethod("https://localhost:44325/api/order/delete/" + ((OrderModel)(item)).Id, string.Empty, "GET");
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
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.RealEstateMethod("https://localhost:44325/api/realestate/get/sell", string.Empty, "GET",tok);
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
            ChangePasswordPage page = new ChangePasswordPage(UserM);
            NavigationService.Navigate(page);
        }

        private async void btnAddMyInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoModel sser = UserM;
            sser.AboutMe = txtAboutMe.Text;
            BaseServices services = new BaseServices();
            ServiceResult res = await services.UserMethod("https://localhost:44325/api/user/edit/" + UserM.Id, JsonConvert.SerializeObject(sser), "PUT", string.Empty);
            if (res.Result == false)
                MessageBox.Show(res.ExceptionMessage);
            else MessageBox.Show(res.Result);
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRealEstatePage page = new AddRealEstatePage(UserM);
            NavigationService.Navigate(page);

            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.RealEstateMethod("https://localhost:44325/api/realestate/get/sell", string.Empty, "GET", tok);
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

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var id = ((RealEstateModel)dgEstates.SelectedItem).Id;
            EditRealEstatePage page = new EditRealEstatePage(UserM, id);
            NavigationService.Navigate(page);

            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.RealEstateMethod("https://localhost:44325/api/realestate/get/sell", string.Empty, "GET", tok);
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

        private void btnAdvertise_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _id = ((RealEstateModel)dgEstates.SelectedItems[0]).Id;
                BaseServices service = new BaseServices();
                string url = $"http://localhost:58446/api/RealEstate/get/byid/{_id}";
                GetRealEstateViewModel model = service.GetEstate(url, "GET");
                AdvertisingModel advModel = new AdvertisingModel()
                {
                    Image = model.Image,
                    StateName = model.StateName,
                    Contacts = model.FullName,
                    Price = model.Price,
                    RealEstateId = model.Id
                };

                HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44399/api/advertising/add");
                request.Method = "POST";
                request.ContentType = "application/json";
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(JsonConvert.SerializeObject(advModel));
                }

                WebResponse response = request.GetResponse();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Select item , {ex}");
            }

        }

        private async void Button_Click_Image(object sender, RoutedEventArgs e)
        {
            UserInfoModel sser = UserM;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter= "Image files (*.jpg) | *.jpg";
            openFile.ShowDialog();
            if (openFile.FileName != null)
            {
                sser.Image = openFile.FileName;
                BaseServices services = new BaseServices();
                ServiceResult res = await services.UserMethod("https://localhost:44325/api/user/edit/" + UserM.Id, JsonConvert.SerializeObject(sser), "PUT", string.Empty);
                if (res.Result == false)
                    MessageBox.Show(res.ExceptionMessage);
                else MessageBox.Show(res.Result);
            }
            else MessageBox.Show("You didn`t choose an image");
            
        }
    }
}
