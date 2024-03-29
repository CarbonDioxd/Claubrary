﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Claubrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(string email)
        {
            InitializeComponent();

            lblWelcomeMessage.Content = "Welcome, " + email;
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Controller.UpdateEmployeeStatus(Auth.EmployeeEmail, false);
            Auth.EmployeeEmail = "";

            new LoginWindow().Show();
            this.Close();
        }

        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            new BooksWindow().ShowDialog();
        }

        private void btnMembers_Click(object sender, RoutedEventArgs e)
        {
            new MemberWindow().ShowDialog();
        }

        private void btnMyProfile_Click(object sender, RoutedEventArgs e)
        {
            new MyProfileWindow().ShowDialog();
        }
    }
}
