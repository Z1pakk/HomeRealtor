﻿using APIConnectService.Service;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using HomeRealtorApi.Models;

namespace RealtorUI
{
    /// <summary>
    /// Interaction logic for ConfirmEmailWindow.xaml
    /// </summary>
    public partial class ConfirmEmailWindow : MetroWindow
    {
        AddUserService service = new AddUserService();

        public ConfirmEmailWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            service.CheckConfirmationCode("https://localhost:44325/api/user/confirmcode", new ConfirmEmailModel()
            {
                Code = tbCode.Text
            });

            LoginWindow window = new LoginWindow();
            this.Visibility = Visibility.Hidden;
            this.Close();
            window.ShowDialog();
        }
    }
}
