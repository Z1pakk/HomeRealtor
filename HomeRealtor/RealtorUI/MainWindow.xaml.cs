using MahApps.Metro.Controls;
using RealtorUI.Pages;
using System.Diagnostics;
using System.IO;
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
    }
}
