using APIConnectService.Helpers;
using APIConnectService.Models;
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
            if (lblPassword.Visibility == Visibility.Visible && txtPassword.Visibility == Visibility.Visible&&txtPassword.Text!="")
            {
                UserInfoModel sser = UserM;
                //sser.Password = txtPassword.Text;
                BaseServices services = new BaseServices();
                ServiceResult res = await services.UserMethod("https://localhost:55945/api/user/edit/" + UserM.Id, JsonConvert.SerializeObject(sser), "PUT", string.Empty);
                if (res.Result == false)
                    MessageBox.Show(res.ExceptionMessage);
                else MessageBox.Show(res.Result);
            }
            else
            if (txtEmail.Text == UserM.Email || txtUserName.Text == UserM.UserName)
            {
                lblPassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Visible;
            }


        }
    }
}
