using RealtorUI.Models;
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
    /// Логика взаимодействия для MyUserInfoPage.xaml
    /// </summary>
    public partial class MyUserInfoPage : Page
    {
        public MyUserInfoPage(UserModel user)
        {
            InitializeComponent();
            imgPerson.Source = new BitmapImage(new Uri("https://localhost:53606/Content/" + user.Image));
            lblName.Content = lblName.Content + user.FirstName + " " + user.LastName;
            lblEmail.Content = lblEmail.Content + user.Email;
            lblAge.Content = lblAge.Content + user.Age.ToString();
            lblPhone.Content = lblPhone.Content + user.PhoneNumber;
        }

        private void btnUpdateE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new page
        }
    }
}
