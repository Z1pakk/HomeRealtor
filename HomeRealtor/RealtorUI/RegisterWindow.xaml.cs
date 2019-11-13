using APIConnectService.Models;
using APIConnectService.Service;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : MetroWindow
    {
        private string ImagePath;
        AddUserService service = new AddUserService();
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                ImagePath = dlg.FileName;
                pbImage.Source = new BitmapImage(new Uri(ImagePath));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                UserModel user = new UserModel()
                {
                    UserName = tbUsrName.Text,
                    FirstName = tbFName.Text,
                    LastName = tbLName.Text,
                    Email = tbEmail.Text,
                    PhoneNumber = tbPhNum.Text,
                    Password = tbPass.Password,
                    Age = int.Parse(tbAge.Text),
                    AboutMe = null,
                    Role = cbRole.Text
                };
                if (!string.IsNullOrEmpty(ImagePath))
                {
                    string image = ImageHelper.ImageToBase64(ImagePath);
                    user.Image = image;
                }

                string token = service.AddUser("https://localhost:44325/api/user/add/", user);
                if (token == "Mail")
                {
                    tbEmail.BorderBrush = Brushes.Red;
                    tbEmail.Text = "";
                    MessageBox.Show("Register failed! This email is alredy used");
                }
                else
                {
                    ConfirmEmailWindow window = new ConfirmEmailWindow();
                    this.Visibility = Visibility.Hidden;
                    this.Close();
                    window.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Register failed! " + ex.Message);
                this.Close();
            }
        }
    }
}
