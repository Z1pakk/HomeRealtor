using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using MahApps.Metro;
using MahApps.Metro.Controls;
using RealtorUI.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RealtorUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindowRealtor.xaml
    /// </summary>
    public partial class MainWindowRealtor : MetroWindow
    {
        public MainWindowRealtor()
        {
            InitializeComponent();
        }
        string Id;
        public MainWindowRealtor(string id)
        {
            InitializeComponent();
            HomePage home = new HomePage();
            frame.Content = home;
            Id = id;
        }

        private void btn_BuyClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new EstateShowPage(Id));
        }

        private void BtnNews_Click(object sender, RoutedEventArgs e)
        {
               
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", "");
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private async void ToggleButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.GetCurrentUser("https://localhost:44325/api/user/current", tok);
            if (res.Success == true)
            {
                UserInfoModel user = (UserInfoModel)res.Result;
                if (user != null)
                    frame.Navigate(new MyRealtorInfoPage(user));
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage home = new HomePage();
            frame.Content = home;
        }
    }
}
