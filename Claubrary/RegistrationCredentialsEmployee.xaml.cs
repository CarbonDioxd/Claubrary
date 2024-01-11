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
    /// Interaction logic for RegistrationCredentialsEmployee.xaml
    /// </summary>
    public partial class RegistrationCredentialsEmployee : Window
    {
        public string Password { get; set; }
        public RegistrationCredentialsEmployee()
        {
            InitializeComponent();
        }

        public RegistrationCredentialsEmployee(string password)
        {
            InitializeComponent();
            Password = password;
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller.UpdateEmployeeDetails(tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text, (DateTime)dpDate.SelectedDate, tbxContactNumber.Text, tbxAddress.Text, Password);
                MessageBox.Show("Registration Complete!");
                
                new LoginWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
