using APIConnectService.Service;
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

namespace AdminUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GetUsersPaginationService service = new GetUsersPaginationService();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void But1_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But1.Content.ToString());
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", nom).ToList();
            if ((int.Parse(But1.Content.ToString()) >= 3))
            {

                But3.Content = But1.Content;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }
        }

        private void But2_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But2.Content.ToString());
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", nom).ToList();
            if (int.Parse(But2.Content.ToString()) >= 3)
            {
                But3.Content = But2.Content;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }
        }

        private void But3_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But3.Content.ToString());
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", nom).ToList();
        }

        private void But4_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But4.Content.ToString());
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", nom).ToList();
            if (int.Parse(But4.Content.ToString()) >= 4)
            {
                But3.Content = But4.Content;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }
        }

        private void But5_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But5.Content.ToString());
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", nom).ToList();
            if (int.Parse(But5.Content.ToString()) >= 5)
            {
                But3.Content = But5.Content;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int ar = int.Parse(txtBox.Text);
            data.ItemsSource = service.GetPagin("http://localhost:44325/api/Admin/GetUserPagin/", ar).ToList();
            But3.Content = ar;
            But4.Content = int.Parse(But3.Content.ToString()) + 1;
            But5.Content = int.Parse(But3.Content.ToString()) + 2;
            But2.Content = int.Parse(But3.Content.ToString()) - 1;
            But1.Content = int.Parse(But3.Content.ToString()) - 2;

            txtBox.Clear();
        }
    }
}
