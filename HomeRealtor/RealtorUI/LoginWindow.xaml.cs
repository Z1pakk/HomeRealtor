using MahApps.Metro.Controls;
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
            try
            {
                if (File.Exists(Directory.GetCurrentDirectory() + @"\token.txt"))
                {


                    var stream = File.ReadAllText(Directory.GetCurrentDirectory() + @"\token.txt");
                    if (stream != "")
                    {

                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(stream);
                        if (jsonToken.ValidTo >= DateTime.Now)
                        {
                            if (File.ReadAllText(Directory.GetCurrentDirectory() + @"\role.txt") == "User")
                            {
                                MainWindow mainWindow = new MainWindow(stream);
                                this.Visibility = Visibility.Hidden;
                                this.Close();
                                mainWindow.ShowDialog();
                                return;
                            }
                            else if (File.ReadAllText(Directory.GetCurrentDirectory() + @"\role.txt") == "Realtor")
                            {
                                MainWindowRealtor mainWindow = new MainWindowRealtor(stream);
                                this.Visibility = Visibility.Hidden;
                                this.Close();
                                mainWindow.ShowDialog();
                                return;
                            }
                            //LoginRoles loginRoles = new LoginRoles(stream);

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            string Role;
            HttpWebRequest request = WebRequest.CreateHttp("https://localhost:44325/api/user/login");
            request.Method = "POST";
            request.ContentType = "application/json";

            Role = rbtnUser.IsChecked==true ? "User": "Realtor";

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                UserLoginModel model = new UserLoginModel();
                writer.Write(JsonConvert.SerializeObject(new UserLoginModel()
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

                sP.Visibility = Visibility.Hidden;
                sP2.Visibility = Visibility.Hidden;
                //btn.Visibility = Visibility.Hidden;
                //lB.Visibility = Visibility.Hidden;
                mE.Visibility = Visibility.Visible;

                string token = await LoginAsync();




                //var tokenS = handler.ReadToken(tokenJwtReponse.access_token) as JwtSecurityToken;
                if (token == "Locked")
                {
                    sP.Visibility = Visibility.Visible;
                    sP2.Visibility = Visibility.Visible;
                    //btn.Visibility = Visibility.Visible;
                    // lB.Visibility = Visibility.Visible;
                    mE.Visibility = Visibility.Hidden;
                    MessageBox.Show("Your account is banned ! Please unlock your account in your email");
                    return;
                }
                if (token == "Role")
                {
                    sP.Visibility = Visibility.Visible;
                    sP2.Visibility = Visibility.Visible;
                    //btn.Visibility = Visibility.Visible;
                    // lB.Visibility = Visibility.Visible;
                    mE.Visibility = Visibility.Hidden;
                    MessageBox.Show("You haven`t got this role");
                    return;
                }
                if (token != "Error")
                {
                    if (rbtnUser.IsChecked == true)
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", token);
                        File.WriteAllText(Directory.GetCurrentDirectory() + @"\role.txt", "User");
                        MainWindow mainWindow = new MainWindow(token);
                        this.Visibility = Visibility.Hidden;
                        this.Close();
                        mainWindow.ShowDialog();
                    } else if (rbtnRealtor.IsChecked == true)
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + @"\token.txt", token);
                        File.WriteAllText(Directory.GetCurrentDirectory() + @"\role.txt", "Realtor");
                        MainWindowRealtor mainWindow = new MainWindowRealtor(token);
                        this.Visibility = Visibility.Hidden;
                        this.Close();
                        mainWindow.ShowDialog();
                    }

                }
                else
                {
                    sP.Visibility = Visibility.Visible;
                    sP2.Visibility = Visibility.Visible;
                    sP.Visibility = Visibility.Visible;
                    //lB.Visibility = Visibility.Visible;
                    mE.Visibility = Visibility.Hidden;
                    passwdBox.BorderBrush = Brushes.Red;
                    passwdBox.Password = "";
                    MessageBox.Show("Incorrect login or password");
                }

            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
                ForgotPasswordWindow window = new ForgotPasswordWindow();
                window.ShowDialog();
            }
        }
    }

