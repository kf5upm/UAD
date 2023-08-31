using System.Windows;
using System.DirectoryServices;
using System;
using System.Diagnostics;

namespace User_Administration_Dashboard
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string adminUser = "";
        private static string adminPass = "";

        private static DirectoryEntry _directoryEntry = new DirectoryEntry("", adminUser, adminPass);

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
