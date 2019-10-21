using System;
using System.Collections.Generic;
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
            string email = emailBox.Text;
            Random rnd = new Random();
            int code = rnd.Next(1000, 9999);
            try
            {
                MailAddress to = new MailAddress(email);
                MailAddress from = new MailAddress("homerealtor@gmail.com", "Home Realtor");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Input this code :";
                m.IsBodyHtml = true;
                m.Body = "Code : " + code + " .";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("homerealtor@gmail.com", "homeRealtor1234");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
