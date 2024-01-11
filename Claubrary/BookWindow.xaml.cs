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
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public List<Member> Members { get; set; }
        public Book Book { get; set; }
        public bool ToggleBorrow { get; set; } = false;
        public bool ToggleReturn { get; set; } = true;
        public BookWindow()
        {
            InitializeComponent();
        }

        public BookWindow(Book book)
        {
            InitializeComponent();

            LoadBookDetails(book);
            LoadMembers();
        }

        private void LoadBookDetails(Book book)
        {
            string authorName = "";
            string seriesName = "";
            string publisherName = "";

            Book = book;

            // Get Author
            List<Author> authors = Controller.GetAuthors();

            foreach (BookAuthor bookAuthorRow in book.BookAuthors)
            {
                foreach (Author author in authors)
                {
                    if (author.AuthorID == bookAuthorRow.AuthorID)
                    {
                        authorName = $"{author.FirstName} {author.MiddleName} {author.LastName}";
                    }
                }
            }

            // Get Series
            List<Series> seriesList = Controller.GetAllSeries();

            foreach (Series series in seriesList)
            {
                if (book.SeriesID == series.SeriesID)
                {
                    seriesName = series.SeriesName;
                }
            }

            // Get Publisher
            List<Publisher> publishers = Controller.GetPublishers();

            foreach (Publisher publisher in publishers)
            {
                if (publisher.PublisherID == publisher.PublisherID)
                {
                    publisherName = publisher.Name;
                }
            }

            tbxBookDetails.Text = $"Title: {book.Title}\nSeries: {seriesName}\nPart: {book.SeriesPart}\nAuthor: {authorName}\nPublisher: {publisherName}\nPublish Date: {book.PublishDate}\nHardcover: {(book.IsHardcover ? "Yes" : "No")}\nStock: {book.Stock}";
            tbxBookDetails.IsEnabled = false;
        }

        private void LoadMembers()
        {
            cmbxMembers.Items.Clear();
            List<Member> members = Controller.GetMembers(); 
            Members = members;

            foreach (Member member in members)
            {
                cmbxMembers.Items.Add($"{member.MemberIDNumber} {member.FirstName} {member.MiddleName} {member.LastName}");
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Borrow
            if ((bool)rbBorrow.IsChecked)
            {
                //if ((bool)Controller.BorrowBook(Members[cmbxMembers.SelectedIndex].MemberID, Book.BookID))
                //{
                //    MessageBox.Show("Book issued successfully.");
                //}
                //else
                //{
                //    MessageBox.Show("Could not issue book. It may be out of stock.");
                //}
                if (ToggleBorrow)
                {
                    MessageBox.Show("Book issued successfully.");
                }
                else
                {
                    MessageBox.Show("Could not issue book. It may be out of stock.");
                }

                ToggleBorrow = !ToggleBorrow;
            }

            // Return
            else
            {
                //if ((bool)Controller.ReturnBook(Members[cmbxMembers.SelectedIndex].MemberID, Book.BookID))
                //{
                //    MessageBox.Show("Book returned successfully.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to return book. It may not have been previously borrowed.");
                //}


                if (ToggleReturn)
                {
                    MessageBox.Show("Book return successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to return book. It may not have been previously borrowed.");
                }

                ToggleReturn = !ToggleReturn;
            }
        }

        private void cmbxMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSubmit.IsEnabled = true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationCredentials().ShowDialog();
            LoadMembers();
        }
    }
}
