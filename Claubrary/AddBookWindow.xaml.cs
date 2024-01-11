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

            PopulateAuthorsCombobox();
            PopulatePublishersCombobox();
            PopulateSeriesCombobox();
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
            // Data Validation
            if (!int.TryParse(tbxSeriesPart.Text, out int seriesPart))
            {
                MessageBox.Show("Please enter a valid series part.");
                return;
            }
            if (!int.TryParse(tbxStock.Text, out int stock))
            {
                MessageBox.Show("Please enter a valid stock number.");
                return;
            }
            if (cbxAuthor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an author.");
                return;
            }
            if (cbxPublisher.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a publisher.");
                return;
            }
            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a publish date.");
                return;
            }

            List<Series> seriesList = Controller.GetAllSeries();

            // Get series ID
            int? seriesID = null;

            for (int i = 0; i < seriesList.Count; i++)
            {
                if (cbxSeries.SelectedIndex == i)
                    seriesID = seriesList[i].SeriesID;
            }

            // Get publisher ID
            List<Publisher> publishers = Controller.GetPublishers();
            int publisherID = -1;

            for (int i = 0; i < publishers.Count; i++)
            {
                if (cbxPublisher.SelectedIndex == i)
                    publisherID = publishers[i].PublisherID;
            }

            // Get AUthor ID
            List<Author> authors = Controller.GetAuthors();
            int authorID = -1;

            for (int i = 0; i < authors.Count; i++)
            {
                if (cbxAuthor.SelectedIndex == i)
                    authorID = authors[i].AuthorID;
            }

            Controller.AddBook(tbxTitle.Text, authorID, (DateTime)dpDate.SelectedDate, (bool)chkbxHardcover.IsChecked, seriesPart, seriesID, publisherID, stock);
        }

        private void PopulatePublishersCombobox()
        {
            List<Publisher> publishers = Controller.GetPublishers();

            foreach (Publisher publisher in publishers)
            {
                cbxPublisher.Items.Add(publisher.Name);
            }
        }

        private void PopulateAuthorsCombobox()
        {
            List<Author> authors = Controller.GetAuthors();

            foreach (Author author in authors)
            {
                cbxAuthor.Items.Add(author.FirstName + ' ' + author.LastName);
            }
        }

        private void PopulateSeriesCombobox()
        {
            List<Series> seriesList = Controller.GetAllSeries();

            foreach (Series series in seriesList)
            {
                cbxSeries.Items.Add(series.SeriesName);
            }
        }
    }
}
