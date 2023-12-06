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

            cmbxSearchBy.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Book> books = Controller.GetBooks();
            List<Book> results = new List<Book>();

            string filter = cmbxSearchBy.SelectedValue.ToString();
            string query = tbxSearch.Text.ToUpper();

            foreach (Book book in books)
            {
                switch (filter)
                {
                    case "Title":
                        if (book.Title.ToUpper().Contains(query) && book.IsHardcover == cbxHardcover.IsChecked)
                            results.Add(book);
                        break;
                    case "Author":
                        List<Author> authors = Controller.GetAuthors();

                        foreach (BookAuthor bookAuthorRow in book.BookAuthors)
                        {
                            foreach (Author author in authors)
                            {
                                string authorName = $"{author.FirstName} {author.MiddleName} {author.LastName}".ToUpper();

                                if (bookAuthorRow.AuthorID == author.AuthorID && 
                                    authorName.ToUpper().Contains(query) && 
                                    book.IsHardcover == cbxHardcover.IsChecked)
                                {
                                    results.Add(book);
                                }
                            }
                        }
                        break;
                    case "Series":
                        List<Series> seriesList = Controller.GetAllSeries();

                        foreach (Series series in seriesList)
                        {
                            if (book.SeriesID == series.SeriesID && 
                                series.SeriesName.ToUpper().Contains(query) && 
                                book.IsHardcover == cbxHardcover.IsChecked)
                            {
                                results.Add(book);
                            }
                        }
                        break;
                    case "Year":
                        int.TryParse(tbxSearch.Text, out int year);

                        if (book.PublishDate.Year == year && book.IsHardcover == cbxHardcover.IsChecked)
                        {
                            results.Add(book);
                        }
                        break;
                    case "Genre":
                        List<Genre> genres = Controller.GetGenres();

                        foreach (BookGenre bookGenreRow in book.BookGenres)
                        {
                            foreach (Genre genre in genres)
                            {
                                if (bookGenreRow.GenreID == genre.GenreID && 
                                    genre.Genre1.ToUpper().Contains(query) && 
                                    book.IsHardcover == cbxHardcover.IsChecked)
                                {
                                    results.Add(book);
                                }
                            }
                        }
                        break;
                }
            }

            lbxBooks.Items.Clear();

            foreach (Book book in results)
            {
                lbxBooks.Items.Add(book.Title);
            }
        }
    }
}
