﻿using AdminkaUI.Pages;
using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
namespace AdminUI
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : MetroWindow
    {
        public MainWindow2()
        {
            InitializeComponent();
            frame.Navigate(new GraphsPage());
        }
        private async void ToggleButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.GetCurrentUser("https://localhost:44325/api/user/current", tok);
            if (res.Success == true)
            {
                UserInfoModel user = (UserInfoModel)res.Result;
            }
        }
        private void BtnPag_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AdminPagination());
        }

        private void BtnBAN_Click(object sender, RoutedEventArgs e)
        {
            
            frame.Navigate(new AdminBanPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnNews_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReal_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new RealEstatePage());
        }

        private void BtnGraphs_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new GraphsPage());
        }

        private async void BtnExit_Click_1(object sender, RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };

            var res=await this.ShowMessageAsync("Are you sure ?", "Are you sure to close window ?", MessageDialogStyle.AffirmativeAndNegative, settings);
            if(res == MessageDialogResult.Affirmative)
            this.Close();
        }

        private void BtnNews(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdvertising_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Advertising());
        }
    }
}
