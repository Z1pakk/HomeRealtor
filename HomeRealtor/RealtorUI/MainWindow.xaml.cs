using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RealtorUI.Models;
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
        EstateShowPage estateSP ;
        public MainWindow()
        {
           
        }
        string Id;
        public MainWindow(string id)
        {

            InitializeComponent();
            HomePage home = new HomePage();
            frame.Content = home;
            Id = id;
            estateSP = new EstateShowPage(Id);
            btnFind.Click += estateSP.Button_ClickAsync;
            cbTown.SelectionChanged += estateSP.cbTown_SelectionChangedAsync;
            cbRegion.SelectionChanged += estateSP.cbRegion_SelectionChangedAsync;
            tbtnFind.Margin = new Thickness(0, 10, this.Width/7.5, 0);

        }


        private void BtnNews_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            frame.Content = aboutUs;
        }

        private void btn_BuyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            frame.Navigate(estateSP);
            
        }

        private void BtnHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HomePage home = new HomePage();
            frame.Content = home;
        }

        private async void BtnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };

            var res = await this.ShowMessageAsync("Are you sure ?", "Are you sure to close window ?", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (res == MessageDialogResult.Affirmative)
            {

                File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", "");
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }
        private async void ToggleButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices services = new BaseServices();
            ServiceResult res = await services.GetCurrentUser("https://localhost:44325/api/user/current", tok);
            if (res.Success == true)
            {
                UserInfoModel user = (UserInfoModel)res.Result;
                if (user != null)
                    frame.Navigate(new MyUserInfoPage(user));
            }
        }

        private void ToggleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if(togle.IsChecked==true)
            {

            ThemeManager.ChangeAppStyle(Application.Current,
                                ThemeManager.GetAccent("Purple"),
                                ThemeManager.GetAppTheme("BaseDark"));
            }
            else
            {

                ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent("Purple"),
                                    ThemeManager.GetAppTheme("BaseLight"));
            }
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            string _token = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
            BaseServices service = new BaseServices();
            string url = "https://localhost:44325/api/RealEstate/get/types";
            List<TypeViewModel> res = service.GetEstateTypes(url, "GET", _token);
            List<ComboBoxModel> temp = res.Select(t => new ComboBoxModel() { Id = t.Id, Name = t.Name }).ToList();
            cbType.ItemsSource = temp;

            url = "https://localhost:44325/api/RealEstate/get/regions";
            List<RegionModel> regions = (await service.GetRegions(url, _token)).Result;
            string er = (await service.GetRegions(url, _token)).ExceptionMessage;
            cbRegion.ItemsSource = regions.Select(t => new ComboBoxModel() { Id = t.Id, Name = t.NameOfRegion }).ToList();
            cbRCount.ItemsSource = new string[] { "1", "2", "3", "4+" };
        }
    }
}
