using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SpecificMemberWindow.xaml
    /// </summary>
    public partial class SpecificMemberWindow : Window
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Borrow> History { get; set; }
        public Member Member { get; set; }
        public bool Toggle { get; set; } = true;

        public SpecificMemberWindow()
        {
            InitializeComponent();
        }
        public SpecificMemberWindow(Member member)
        {
            InitializeComponent();

            LoadFields(member);
            LoadHistory(member);

            Member = member;
        }

        private void LoadFields(Member member)
        {
            tbxFirstName.Text = member.FirstName;
            tbxMiddleName.Text = member.MiddleName;
            tbxLastName.Text = member.LastName;
            tbxAddress.Text = member.Address;
            tbxEmail.Text = member.Email;
            tbxContactNumber.Text = member.ContactNumber;
            dpBirthDate.SelectedDate = member.BirthDate;

            tbxFirstName.IsEnabled = false;
            tbxMiddleName.IsEnabled = false;
            tbxLastName.IsEnabled = false;
            tbxAddress.IsEnabled = false;
            dpBirthDate.IsEnabled = false;
            tbxEmail.IsEnabled = false;
            tbxContactNumber.IsEnabled = false;
        }

        private void LoadHistory(Member member)
        {
            List<Borrow> borrows = Controller.GetBorrowsFromMember(member);
            History = borrows;

            foreach (Borrow borrow in borrows)
            {
                int due = borrow.DueDate.Subtract(DateTime.Today).Days;
                if (borrow.ReturnDate == null)
                    lbxHistory.Items.Add($"{borrow.Book.Title} ({borrow.BorrowDate}) - Due in {(due < 0 ? "OVERDUE" : due.ToString()) + " day/s"}");
            }
        }

        private void dpFirstDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            StartDate = (DateTime)dpFirstDate.SelectedDate;
        }

        private void dpSecondDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            EndDate = (DateTime)dpSecondDate.SelectedDate;

            if (StartDate != null)
            {
                lbxHistory.Items.Clear();

                // Filter History
                List<Borrow> borrows = Controller.GetBorrowsFromMember(Member);

                foreach (Borrow borrow in borrows)
                {
                    switch ((bool)chkbxReturned.IsChecked)
                    {
                        case true:
                            if (borrow.ReturnDate == null)
                                continue;
                            if (borrow.BorrowDate >= StartDate && (DateTime)borrow.ReturnDate <= EndDate)
                            {
                                int due = borrow.DueDate.Subtract(DateTime.Today).Days;
                                lbxHistory.Items.Add($"{borrow.Book.Title} ({borrow.BorrowDate}) - Due in {(due < 0 ? "OVERDUE" : due.ToString()) + " day/s"}");
                            }
                            break;
                        case false:
                            if (borrow.BorrowDate >= StartDate && borrow.BorrowDate <= EndDate)
                            {
                                int due = borrow.DueDate.Subtract(DateTime.Today).Days;
                                lbxHistory.Items.Add($"{borrow.Book.Title} ({borrow.BorrowDate}) - Due in {(due < 0 ? "OVERDUE" : due.ToString()) + " day/s"}");
                            }
                            break;
                    }
                }

            }

            //if (Toggle)
            //    lbxHistory.Items.Clear();
            //else
            //    LoadHistory(Member);

            //Toggle = !Toggle;
        }

        private void chkbxReturned_Checked(object sender, RoutedEventArgs e)
        {
            lbxHistory.Items.Clear();

            // Filter History
            List<Borrow> borrows = Controller.GetBorrowsFromMember(Member);

            foreach (Borrow borrow in borrows)
            {
                if (borrow.ReturnDate == null)
                    continue;

                int due = borrow.DueDate.Subtract(DateTime.Today).Days;
                lbxHistory.Items.Add($"{borrow.Book.Title} ({borrow.BorrowDate}) - Due in {(due < 0 ? "OVERDUE" : due.ToString()) + " day/s"}");
         
            }
        }

        private void chkbxReturned_Unchecked(object sender, RoutedEventArgs e)
        {
            lbxHistory.Items.Clear();

            // Filter History
            List<Borrow> borrows = Controller.GetBorrowsFromMember(Member);

            foreach (Borrow borrow in borrows)
            {
                int due = borrow.DueDate.Subtract(DateTime.Today).Days;
                lbxHistory.Items.Add($"{borrow.Book.Title} ({borrow.BorrowDate}) - Due in {(due < 0 ? "OVERDUE" : due.ToString()) + " day/s"}");
            }
        }
    }
}
