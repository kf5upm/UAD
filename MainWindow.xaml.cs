using System.Windows;
using System.DirectoryServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections;

namespace User_Administration_Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private static string adminUser = "";
        //private static string adminPass = "";
        //private static DirectoryEntry _directoryEntry = new DirectoryEntry("", adminUser, adminPass);

        // This object is based on the currently logged in user and their permissions in Active Directory.
        // The examples above are for specifying a different user in case this tool is to be used by unprivileged users.
        // Another way of making this connection is via an LDAP reference as shown here:
        //private static DirectoryEntry _directoryEntry = new DirectoryEntry("LDAP://RootDSE");
        private static DirectoryEntry _directoryEntry = new DirectoryEntry();

        public MainWindow()
        {
            _directoryEntry.AuthenticationType = AuthenticationTypes.Secure;
            InitializeComponent();


        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DirectorySearcher searcher = new DirectorySearcher(_directoryEntry);

            string searchParameter = SearchParameter.Text;
            searcher.Filter = $"(&(objectClass=User)(displayName={searchParameter}*))";

            SearchResultCollection searchResults = searcher.FindAll();

            if (searchResults.Count == 0)
            {
                MessageBox.Show("No users matching that search criteria were found.");
            }
            // TODO:  Add a list to select the desired user from
            else if (searchResults.Count > 1)
            {
                foreach(SearchResult result in searchResults)
                {
                    DirectoryEntry de = result.GetDirectoryEntry();
                    Debug.WriteLine(de.Name);
                }
                MessageBox.Show($"{searchResults.Count} resuls found");
            }
            else
            {
                DirectoryEntry de = searchResults[0].GetDirectoryEntry();

                ADUser currentUser = new ADUser(de);
                this.DataContext = currentUser;
            }
        }
    }
}
