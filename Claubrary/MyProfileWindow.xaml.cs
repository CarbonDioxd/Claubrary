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
                if (employee1.Email == Auth.EmployeeEmail)
                    employee = employee1;
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
            // Password is to be changed
            if (tbxCurrentPassword.Text.Length > 0 || tbxNewPassword.Text.Length > 0 || tbxConfirmNewPassword.Text.Length > 0)
            {
                if (tbxCurrentPassword.Text.Length < 1)
                {
                    MessageBox.Show("Please enter your current password to change passwords");
                    return;
                }
                if (tbxNewPassword.Text.Length < 1)
                {
                    MessageBox.Show("Please enter your new password.");
                    return;
                }
                if (tbxConfirmNewPassword.Text.Length < 1)
                {
                    MessageBox.Show("Please confirm your new password.");
                    return;
                }
                if (tbxNewPassword.Text != tbxConfirmNewPassword.Text) 
                {
                    MessageBox.Show("New Passsword and Password Confirmation does not match.");
                    return;
                }
            }

            // Find employeeID
            List<Employee> employees = Controller.GetEmployees();
            int employeeID = -1;
            string password = "";

            foreach (Employee employee in employees)
            {
                if (employee.Email == Auth.EmployeeEmail)
                {
                    employeeID = employee.EmployeeID;
                    password = employee.Password;
                }
            }


            // Password was changed
            if (tbxCurrentPassword.Text.Length > 0 || tbxNewPassword.Text.Length > 0 || tbxConfirmNewPassword.Text.Length > 0)
            {
                if (tbxCurrentPassword.Text != password)
                {
                    Controller.Context.uspUpdateEmployeeDetails(employeeID, tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text, dpBirthDate.SelectedDate, tbxContactNumber.Text, tbxAddress.Text, password);
                    MessageBox.Show("Details updated. Password changed unsuccessfully (Wrong Current Password).");
                }
                else
                {
                    Controller.Context.uspUpdateEmployeeDetails(employeeID, tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text, dpBirthDate.SelectedDate, tbxContactNumber.Text, tbxAddress.Text, tbxNewPassword.Text);
                    MessageBox.Show("Details updated. Password changed successfully.");
                }
            }
            else
            {
                Controller.Context.uspUpdateEmployeeDetails(employeeID, tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text, dpBirthDate.SelectedDate, tbxContactNumber.Text, tbxAddress.Text, password);
                MessageBox.Show("Details updated successfully.");
            }
        }
    }
}
