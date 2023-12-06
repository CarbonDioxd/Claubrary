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
using System.Windows.Shapes;

namespace Claubrary
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class EmployeeRegistrationWindow : Window
    {
        public EmployeeRegistrationWindow()
        {
            InitializeComponent();
        }

        private void btnSendOTP_Click(object sender, RoutedEventArgs e)
        {
            // Validate Email Field
            if (tbxEmail.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please fill out the email field.");
                return;
            }
            if (Controller.IsEmployeVerifiedOrExists(tbxEmail.Text))
            {
                MessageBox.Show("Email already exists or is already verified. Please log in.");
                return;
            }

            SMTP.SendEmail(
                    "viraylogan@gmail.com",
                    tbxEmail.Text,
                    "Claubrary One-time Password (OTP)",
                    Controller.SendOTP(tbxEmail.Text, tbxPassword.Text)
                );

            btnVerifyOTP.IsEnabled = true;
            btnSendOTP.IsEnabled = false;
        }

        private void btnVerifyOTP_Click(object sender, RoutedEventArgs e)
        {
            // Handle No Password
            if (tbxPassword.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please fill out the password field.");
                return;
            }

            // Handle Successful OTP
            if (Controller.VerifyOTP(tbxEmail.Text, tbxOTP.Text))
            {
                Controller.VerifyEmployee(tbxEmail.Text, tbxPassword.Text);
                MessageBox.Show("Email successfully verified");

                new RegistrationCredentialsEmployee().Show();
                this.Close();
            }
            else
            {
                // Handle Failed OTP
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        private void btnLoginHere_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}
