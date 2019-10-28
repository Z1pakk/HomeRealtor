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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        string _token;
        string _ownerName;
        int _id;
        public OrderPage(string token, string ownerName, int id)
        {
            InitializeComponent();
            _token = token;
            _ownerName = ownerName;
            txt_Name.Text += _ownerName;
            _id = id;
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            BaseServices service = new BaseServices();
            string url = $"https://localhost:44325/api/Order/add/{_id}";
            AddOrderViewModel model = new AddOrderViewModel()
            {
                ApartId = _id,
                Message = this.textBoxMess.Text
            };
            await service.AddOrderMethod(url, JsonConvert.SerializeObject(model), "POST", _token);
        }
    }
}
