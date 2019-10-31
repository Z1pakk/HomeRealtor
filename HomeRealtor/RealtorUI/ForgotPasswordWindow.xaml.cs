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
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/user/sendcode");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                SendCodeModel model = new SendCodeModel();
                writer.Write(JsonConvert.SerializeObject(new SendCodeModel()
                {
                    Email = emailBox.Text
                }));
            }
            WebResponse response = request.GetResponse();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/user/checkcode");
            request.Method = "GET";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                CheckCodeModel model = new CheckCodeModel();
                writer.Write(JsonConvert.SerializeObject(new CheckCodeModel()
                {
                    Code = codeBox.Text,
                    NewPassword = newPasswordBox.Password
                }));
            }
            WebResponse response = request.GetResponse();
        }
    }
}
