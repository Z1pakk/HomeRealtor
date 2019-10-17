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

namespace RealtorUI
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Forgot_Password(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            this.Visibility = Visibility.Hidden;
            this.Close();
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:54365/api/user/login");
            request.Method = "POST";
            request.ContentType = "application/json";
            
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                UserLoginModel model = new UserLoginModel();
                writer.Write(JsonConvert.SerializeObject(new UserLoginModel()
                {
                    
                    Password=passwdBox.Password,
                    Email=loginBox.Text
                }));
            }
            WebResponse response = request.GetResponse();

            string userId;
            using (StreamReader reader=new StreamReader(response.GetResponseStream()))
            {
                string temp = reader.ReadToEnd();
                userId=temp;
            }
            if(userId!= "Error")
            {

                MainWindow mainWindow = new MainWindow(userId);
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
