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
    /// Interaction logic for BooksWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        public BooksWindow()
        {
            InitializeComponent();

            LoadBooks();
            LoadSearchComboBox();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadBooks()
        {
            List<Book> books = Controller.GetBooks();

            foreach (Book book in books)
            {
                lbxBooks.Items.Add(book.Title);
            }
        }
        private void LoadSearchComboBox()
        {
            List<string> categories = new List<string>()
            {
                "Title",
                "Author",
                "Series",
                "Year",
                "Genre",
            };

            foreach (string category in categories)
            {
                cmbxSearchBy.Items.Add(category);
            }
        }
    }
}
