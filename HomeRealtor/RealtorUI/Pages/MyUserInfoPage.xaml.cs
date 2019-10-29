﻿using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using Newtonsoft.Json;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MyUserInfoPage.xaml
    /// </summary>
    public partial class MyUserInfoPage : Page
    {
        public UserInfoModel UserM { get; set; }
        public MyUserInfoPage(UserInfoModel user)
        {
            InitializeComponent();
            UserM = user;
            imgPerson.Source = new BitmapImage(new Uri("https://localhost:44325/Content/" + user.Image));
            lblName.Content = lblName.Content + user.FirstName + " " + user.LastName;
            lblEmail.Content = lblEmail.Content + user.Email;
            lblAge.Content = lblAge.Content + user.Age.ToString();
            lblPhone.Content = lblPhone.Content + user.PhoneNumber;
        }

        private async void btnUpdateR_Click(object sender, RoutedEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult resEstate = await services.RealEstateMethod("https://localhost:44325/api/realestate/get/rent", string.Empty, "GET",tok);
            ServiceResult resEstate2 = await services.RealEstateMethod("https://localhost:44325/api/realestate/get/sublease", string.Empty, "GET",tok);
            ServiceResult resOrder = await services.OrderMethod("https://localhost:44325/api/order/orders", string.Empty, "GET");

            if (resEstate.Success == true || resEstate2.Success == true)
            {
                if (resOrder.Success == true)
                {
                    dgRent.Items.Clear();
                    foreach (var item in resOrder.Result)
                    {
                        if (((OrderModel)(item)).UserId == UserM.Id.ToString())
                        {
                            foreach (var it in resEstate.Result)
                            {
                                if (((RealEstateModel)(it)).Id == ((OrderModel)(item)).ApartId)
                                {
                                    RealEstateModel model = it;
                                    model.Image = "https://localhost:44325/Content/" + model.Image;
                                    dgRent.Items.Add(model);
                                }
                            }
                            foreach (var it in resEstate2.Result)
                            {
                                if (((RealEstateModel)(it)).Id == ((OrderModel)(item)).ApartId)
                                {
                                    RealEstateModel model = it;
                                    model.Image = "https://localhost:44325/Content/" + model.Image;
                                    dgRent.Items.Add(model);
                                }
                            }
                        }
                    }

                }
                else MessageBox.Show(resOrder.ExceptionMessage);

            }
            else MessageBox.Show(resEstate.ExceptionMessage);
        }

        private async void btnCancelR_Click(object sender, RoutedEventArgs e)
        {
            if (dgRent.SelectedItem != null)
            {
                BaseServices services = new BaseServices();
                ServiceResult resOrder = await services.OrderMethod("https://localhost:44325/api/order/orders", string.Empty, "GET");
                if (resOrder.Success == true)
                {
                    foreach (var item in resOrder.Result)
                    {
                        //if (((OrderModel)(item)).ApartId == ((RealViewEstateModel)dgRent.SelectedItem).Id)
                        if (((OrderModel)(item)).ApartId == ((RealEstateModel)dgRent.SelectedItem).Id)
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
    }
}
