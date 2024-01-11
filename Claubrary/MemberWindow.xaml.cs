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
            new SpecificMemberWindow(Members[lbxMembers.SelectedIndex]).ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!ToggleSearch)
            {
                LoadMembers();
            }
            else
            {
                lbxMembers.Items.Clear();
            }

            ToggleSearch = !ToggleSearch;
        }
    }
}
