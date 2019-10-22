using MahApps.Metro.Controls;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealtorUI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            if (File.Exists(Directory.GetCurrentDirectory() + @"\token.txt"))
            {


                var stream = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
                if (stream != "")
                {

                    //var handler = new JwtSecurityTokenHandler();
                    //var jsonToken = handler.ReadToken(stream);
                    //if (jsonToken.ValidTo >= DateTime.Now)
                    //{
                        //MainWindow mainWindow = new MainWindow(stream);
                        //this.Visibility = Visibility.Hidden;
                        //this.Close();
                        //mainWindow.ShowDialog();
                        //return;
                    //}
                }
            }
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
        private async Task<string> LoginAsync()
        {

            HttpWebRequest request = WebRequest.CreateHttp("http://localhost:54365/api/user/login");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                UserLoginModel model = new UserLoginModel();
                writer.Write(JsonConvert.SerializeObject(new UserLoginModel()
                {

                    Password = passwdBox.Password,
                    Email = loginBox.Text
                }));
            }
            WebResponse response =await request.GetResponseAsync();

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

            sP.Visibility = Visibility.Hidden;
            sP2.Visibility = Visibility.Hidden;
            lB.Visibility = Visibility.Hidden;
            mE.Visibility = Visibility.Visible;

            string token=await LoginAsync();

           
            //var tokenS = handler.ReadToken(tokenJwtReponse.access_token) as JwtSecurityToken;
            if (token != "Error")
            {

                File.WriteAllText( Directory.GetCurrentDirectory()+@"\token.txt", token);
                MainWindow mainWindow = new MainWindow(token);
                this.Visibility = Visibility.Hidden;
                this.Close();
                mainWindow.ShowDialog();
            }
            else
            {
                sP.Visibility = Visibility.Visible;
                sP2.Visibility = Visibility.Visible;
                lB.Visibility = Visibility.Visible;
                mE.Visibility = Visibility.Hidden;
                passwdBox.BorderBrush = Brushes.Red;
                passwdBox.Password = "";
               MessageBox.Show("Incorrect login or password");
            }
          
        }

        

        
    }
}
