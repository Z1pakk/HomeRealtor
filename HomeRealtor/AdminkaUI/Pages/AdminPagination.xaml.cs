﻿using AdminUI.Model;
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
        public AdminPagination()
        {
            InitializeComponent();
        }
        private void But1_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But1.Content.ToString());
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
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
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
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
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
        }

        private void But4_Click(object sender, RoutedEventArgs e)
        {
            int nom = int.Parse(But4.Content.ToString());
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
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
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
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
            int nom = int.Parse(txtBox.Text);
            string get = service.GetPagin("https://localhost:44325/api/Admin/GetUserPagin/", nom);
            List<AdminHelpModel> list = JsonConvert.DeserializeObject<List<AdminHelpModel>>(get);
            data.ItemsSource = list;
            if (nom >= 3)
            {
                But3.Content = nom;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }
            else
            {
                But3.Content = 3;
                But4.Content = int.Parse(But3.Content.ToString()) + 1;
                But5.Content = int.Parse(But3.Content.ToString()) + 2;
                But2.Content = int.Parse(But3.Content.ToString()) - 1;
                But1.Content = int.Parse(But3.Content.ToString()) - 2;
            }

            txtBox.Clear();
        }
    }
}
