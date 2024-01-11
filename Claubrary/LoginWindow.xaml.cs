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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            new EmployeeRegistrationWindow().Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LogIn();
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                LogIn();
                this.Close();
            }
        }

        private void LogIn()
        {
            // Validate Fields
            if (tbxEmail.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please fill out the email field");
                return;
            }
            if (pbxPassword.Password.Trim().Length < 1)
            {
                MessageBox.Show("Please fill out the password field");
                return;
            }

            if (!Controller.SignInEmployee(tbxEmail.Text, pbxPassword.Password))
            {
                MessageBox.Show("Invalid username or password. Please try again.");
                return;
            }

            Controller.UpdateEmployeeStatus(tbxEmail.Text, true);
            Auth.EmployeeEmail = tbxEmail.Text;

            new MainWindow(tbxEmail.Text).Show();
        }
    }
}
