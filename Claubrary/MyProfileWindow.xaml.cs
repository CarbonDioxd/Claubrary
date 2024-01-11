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
    /// Interaction logic for MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        public MyProfileWindow()
        {
            InitializeComponent();
            LoadEmployeeDetails();
        }

        private void LoadEmployeeDetails()
        {
            Employee employee = new Employee();

            foreach (Employee employee1 in Controller.Context.Employees)
            {
                employee = employee1 as Employee;
            }

            tbxFirstName.Text = employee.FirstName;
            tbxMiddleName.Text = employee.MiddleName;
            tbxLastName.Text = employee.LastName;
            tbxAddress.Text = employee.Address;
            tbxContactNumber.Text = employee.ContactNumber;
            dpBirthDate.SelectedDate = employee.BirthDate;

            dpBirthDate.IsEnabled = false;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (tbxNewPassword.Text.Length > 0 && tbxCurrentPassword.Text.Length < 1)
            {
                MessageBox.Show("Please enter your current password to change passwords");
                return;
            }
            if (tbxConfirmNewPassword.Text.Length > 0 && tbxCurrentPassword.Text.Length < 1)
            {
                MessageBox.Show("Please enter your current password to change passwords");
                return;
            }
            if (tbxNewPassword.Text.Length > 0 && tbxConfirmNewPassword.Text.Length < 1)
            {
                MessageBox.Show("Please confirm you new password.");
                return;
            }

            Controller.Context.uspUpdateEmployeeDetails(tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text, dpBirthDate.SelectedDate, tbxContactNumber.Text, tbxAddress.Text, tbxNewPassword.Text);
            MessageBox.Show("Password changed successfully.");
        }
    }
}
