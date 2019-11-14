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

namespace AdminkaUI.Pages
{
    /// <summary>
    /// Interaction logic for AdminBanPage.xaml
    /// </summary>
    public partial class AdminBanPage : Page
    {
        GetUsersPaginationService service = new GetUsersPaginationService();
        string ema;
        public AdminBanPage(string emailMy)
        {
            InitializeComponent();
            ema = emailMy;
        }

        private void ButAccept_Click(object sender, RoutedEventArgs e)
        {
            if (ema != BanEmail.Text)
            {
                string email = BanEmail.Text;
                string get = service.Ban("https://localhost:44325/api/Admin/ban/", email);
                answer.Text = get;
            }
            else { answer.Text = "You can`t ban yourself!!!!BAKA"; }
        }
    }
}
