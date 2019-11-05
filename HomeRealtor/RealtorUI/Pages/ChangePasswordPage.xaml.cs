using APIConnectService.Helpers;
using APIConnectService.Models;
using APIConnectService.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealtorUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        public UserInfoModel UserM { get; set; }
        public ChangePasswordPage(UserInfoModel u)
        {
            InitializeComponent();
            UserM = u;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((lblOldPassword.Visibility == Visibility.Visible && txtOldPassword.Visibility == Visibility.Visible&&txtOldPassword.Password != "")
                && lblNewPassword.Visibility == Visibility.Visible && txtNewPassword.Visibility == Visibility.Visible && txtNewPassword.Password != "")
            {
                string tok = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
                string[] Passwords = new string[2] { txtOldPassword.Password,txtNewPassword.Password };
                BaseServices services = new BaseServices();
                ServiceResult res = await services.UserMethod("https://localhost:44325/api/user/change", JsonConvert.SerializeObject(Passwords), "PUT", tok);
                if (res.Success == false)
                    MessageBox.Show(res.ExceptionMessage);
                else MessageBox.Show(res.Result);
            }
            else
            if (txtEmail.Text == UserM.Email || txtUserName.Text == UserM.UserName)
            {
                lblNewPassword.Visibility = Visibility.Visible;
                txtNewPassword.Visibility = Visibility.Visible;
                lblOldPassword.Visibility = Visibility.Visible;
                txtOldPassword.Visibility = Visibility.Visible;
            }


        }
    }
}
