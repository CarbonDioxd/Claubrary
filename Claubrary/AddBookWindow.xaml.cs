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
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
        }

        private void btnAddPublisher_Click(object sender, RoutedEventArgs e)
        {
            new InputWindow("publisher").ShowDialog();
        }

        private void btnAddSeries_Click(object sender, RoutedEventArgs e)
        {
            new InputWindow("series").ShowDialog();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            new InputWindow().ShowDialog();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Book added successfully!");
        }
    }
}
