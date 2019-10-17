using MahApps.Metro.Controls;
using RealtorUI.Pages;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        string Id;
        public MainWindow(string id)
        {
            InitializeComponent();
            Id = id;
        }
        private void BtnNews_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           // AboutUs aboutUs = new AboutUs();
            //frame.Content = aboutUs;
        }

        private void BtnHome_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void btn_BuyClick(object sender, System.Windows.RoutedEventArgs e)
        {
            frame.Navigate(new EstateShowPage());
        }
    }
}
