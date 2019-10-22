using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using MahApps.Metro.Controls;
using RealtorUI.Pages;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
           
        }
        string Id;
        public MainWindow(string id)
        {
            InitializeComponent();
            Id = id;
        }
        private void BtnNews_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //AboutUs aboutUs = new AboutUs();
            //frame.Content = aboutUs;
        }

        private void btn_BuyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            frame.Navigate(new EstateShowPage());
            
        }
        private void BtnHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", "");
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }


        private async void ToggleButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BaseServices services = new BaseServices();
            ServiceResult res = await services.UserMethod("https://localhost:55945/api/user/get", string.Empty, "GET");
            if (res.Success == true)
            {
                List<UserModel> users = (List<UserModel>)res.Result;
                UserModel user = users.FirstOrDefault(t => t.Id == Id);
                if (user != null)
                    frame.Navigate(new MyUserInfoPage(user));
            }
        }
    }
}
