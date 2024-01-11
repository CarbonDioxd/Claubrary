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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        public List<Member> Members { get; set; }
        public bool ToggleSearch { get; set; } = false;

        public MemberWindow()
        {
            InitializeComponent();
            LoadMembers();
            LoadSearch();
        }

        private void LoadSearch()
        {
            List<string> filters = new List<string>() 
            {
                "Name",
                "Member ID Number",
                "Email Address",
                "Contact Number"
            };

            foreach (string filter in filters)
            {
                cmbxSearchBy.Items.Add(filter);
            }
        }

        private void LoadMembers()
        {
            lbxMembers.Items.Clear();

            List<Member> members = Controller.GetMembers(); 
            Members = members;

            foreach (Member member in members)
            {
                lbxMembers.Items.Add($"{member.MemberIDNumber} {member.FirstName} {member.MiddleName} {member.LastName}");
            }
        }

        private void lbxMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxMembers.SelectedIndex == -1)
                return;

            new SpecificMemberWindow(Members[lbxMembers.SelectedIndex]).ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }

        private void Search()
        {
            List<Member> membersList = Controller.GetMembers();
            List<Member> results = new List<Member>();

            string filter = cmbxSearchBy.SelectedValue.ToString();
            string query = tbxSearch.Text.ToUpper();

            foreach (Member member in membersList)
            {
                switch (filter)
                {
                    case "Name":
                        string fullName = $"{member.FirstName} {member.MiddleName} {member.LastName}".ToUpper();

                        if (fullName.Contains(query))
                            results.Add(member);
                        break;
                    case "Member ID Number":
                        if (int.TryParse(query, out int memIdNum) && memIdNum == member.MemberIDNumber)
                            results.Add(member);
                        break;
                    case "Email Address":
                        if (member.Email.ToUpper().Contains('@') && member.Email.ToUpper().Contains(query))
                            results.Add(member);
                        break;
                    case "Contact Number":
                        if (member.ContactNumber.Contains(query))
                            results.Add(member);
                        break;
                }
            }

            // Refresh Display
            lbxMembers.Items.Clear();

            Members = results;

            List<Member> members = Controller.GetMembers();

            foreach (Member member in results)
            {
                lbxMembers.Items.Add($"{member.MemberIDNumber} {member.FirstName} {member.MiddleName} {member.LastName}");
            }


        }
    }
}
