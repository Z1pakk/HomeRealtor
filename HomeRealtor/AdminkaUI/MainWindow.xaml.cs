using AdminUI;
using AdminUI.Model;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminkaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async Task<string> LoginAsync()
        {
            string Role = "Admin";
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/Admin/login");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                AdminLoginModel model = new AdminLoginModel();
                writer.Write(JsonConvert.SerializeObject(new AdminLoginModel()
                {

                    Password = passwdBox.Password,
                    Email = loginBox.Text,
                    Role = Role
                }));
            }
            WebResponse response = await request.GetResponseAsync();

            string token;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string temp = reader.ReadToEnd();
                token = temp;
            }
            return token;
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        { 
            string token = await LoginAsync();




            //var tokenS = handler.ReadToken(tokenJwtReponse.access_token) as JwtSecurityToken;
            if (token == "Locked")
            {
                MessageBox.Show("Your account is banned ! Please unlock your account in your email");
                return;
            }
            if (token == "Role")
            {
                MessageBox.Show("You haven`t got this role");
                return;
            }
            if (token != "Error")
            {
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", token);
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\role.txt", "Admin");
                    MainWindow2 mainWindow = new MainWindow2();
                    this.Visibility = Visibility.Hidden;
                    this.Close();
                    mainWindow.ShowDialog();

            }
            else
            {
                passwdBox.BorderBrush = Brushes.Red;
                passwdBox.Password = "";
                MessageBox.Show("Incorrect login or password");
            }

        }

    }
}
