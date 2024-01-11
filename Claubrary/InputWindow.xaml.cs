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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public static string Settings { get; set; } = "series";

        public InputWindow()
        {
            InitializeComponent();

            switch (Settings.ToLower())
            {
                case "publisher":
                    lblFirstName.Content = "Name";
                    lblMiddleName.Content = "Addresss";
                    lblLastName.Visibility = Visibility.Hidden;
                    tbxLastName.Visibility = Visibility.Hidden;
                    break;
                case "series":
                    lblFirstName.Content = "Name";
                    lblMiddleName.Content = "Parts No.";
                    lblLastName.Visibility = Visibility.Hidden;
                    tbxLastName.Visibility = Visibility.Hidden;
                    break;
            }
        }

        public InputWindow(string addSettings)
        {
            InitializeComponent();

            Settings = addSettings;

            switch (Settings.ToLower())
            {
                case "publisher":
                    lblFirstName.Content = "Name";
                    lblMiddleName.Content = "Addresss";
                    lblLastName.Visibility = Visibility.Hidden;
                    tbxLastName.Visibility = Visibility.Hidden;
                    break;
                case "series":
                    lblFirstName.Content = "Name";
                    lblMiddleName.Content = "Parts No.";
                    lblLastName.Visibility = Visibility.Hidden;
                    tbxLastName.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            switch (Settings.ToLower())
            {
                case "author":
                    Controller.Context.uspAddAuthor(tbxFirstName.Text, tbxMiddleName.Text, tbxLastName.Text);
                    MessageBox.Show("Author added successfully!");
                    break;
                case "publisher":
                    Controller.Context.uspAddPublisher(tbxFirstName.Text, tbxMiddleName.Text);
                    MessageBox.Show("Publisher added successfully!");
                    break;
                case "series":
                    Controller.Context.uspAddSeries(tbxFirstName.Text, int.Parse(tbxMiddleName.Text));
                    MessageBox.Show("Series added successfully!");
                    break;
            }
        }
    }
}
