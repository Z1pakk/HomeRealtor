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
            UserModel user = new UserModel()
            {
                UserName = tbUsrName.Text,
                FirstName = tbFName.Text,
                LastName = tbLName.Text,
                Email = tbEmail.Text,
                PhoneNumber = tbPhNum.Text,
                Password = tbPass.Password,
                Age = int.Parse(tbAge.Text),
                Image = ImagePath,
                AboutMe = null,
                Role = cbRole.Text
            };
            service.AddUser("http://localhost:60946/api/user/add/", user);

            this.Close();
            LoginWindow window = new LoginWindow();
            window.ShowDialog();

        }
    }
}
