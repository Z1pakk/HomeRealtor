using AdminUI.Model;
using APIConnectService.Service;
using Newtonsoft.Json;
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
    /// Interaction logic for AdminPagination.xaml
    /// </summary>
    public partial class AdminPagination : Page
    {
        GetUsersPaginationService service = new GetUsersPaginationService();
        int count;
        int thisbut=1;
        public AdminPagination()
        {
            InitializeComponent();
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", 1);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;

        }
        private void But1_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But1.Content.ToString());
            thisbut = nom;
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;
            if ((int.Parse(But1.Content.ToString()) >= 3))
            {
                Metod(nom);
            }
            else { Metod(3); }
        }

        private void But2_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But2.Content.ToString());
            thisbut = nom;
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;
            if (int.Parse(But2.Content.ToString()) >= 3)
            {
                Metod(nom);
            }
            else { Metod(3); }
        }

        private void But3_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But3.Content.ToString());
            thisbut = nom;
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;
        }

        private void But4_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But4.Content.ToString());
            thisbut = nom;
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;
            if (int.Parse(But4.Content.ToString()) >= 4)
            {
                Metod(nom);
            }
        }

        private void But5_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But5.Content.ToString());
            thisbut = nom;
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            count = list.Count;
            if (int.Parse(But5.Content.ToString()) >= 5)
            {
                Metod(nom);
            }
        }
        private void Metod(int num)
        {
            But3.Content = num;
            But4.Content = int.Parse(But3.Content.ToString()) + 1;
            But5.Content = int.Parse(But3.Content.ToString()) + 2;
            But2.Content = int.Parse(But3.Content.ToString()) - 1;
            But1.Content = int.Parse(But3.Content.ToString()) - 2;
            
        }

        private void Data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (count >= (thisbut - 1) * 10)
            {
                var item = (AdminHelpModel)data.SelectedItems[0];
                if (item.Path != null)
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(item.Path);
                    bi3.EndInit();
                    image.Source = bi3;
                }
                else { image.Source = null; }
                AboutMe.Text = item.AboutMy;
            }
            else { AboutMe.Text = ""; image.Source = null; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", 1);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            thisbut = 1;
            AboutMe.Text = ""; image.Source = null;
            Metod(3);
        }
    }
}
