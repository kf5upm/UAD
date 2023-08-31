using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;

// TODO: Convert this to use the newer System.DirectoryServices.AccountManagement approach.
// https://learn.microsoft.com/en-us/dotnet/api/system.directoryservices.accountmanagement?view=dotnet-plat-ext-7.0&redirectedfrom=MSDN
//

namespace User_Administration_Dashboard
{
    class ADUser : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DirectoryEntry _directoryEntry;

        public ADUser(DirectoryEntry directoryEntry)
        {
            _directoryEntry = directoryEntry;
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public string FirstName
        {
            get { return _directoryEntry.Properties["givenname"].Value?.ToString(); }
        }
        public string LastName
        {
            get { return _directoryEntry.Properties["sn"].Value?.ToString(); }
        }

        public string EmailAddress
        {
            get { return _directoryEntry.Properties["mail"].Value?.ToString(); }
        }

        public string City
        {
            get { return _directoryEntry.Properties["l"].Value?.ToString(); }
        }

        public string State
        {
            get { return _directoryEntry.Properties["st"].Value?.ToString(); }
        }

        public string Title
        {
            get { return _directoryEntry.Properties["title"].Value?.ToString(); }
        }
        public string Department
        {
            get { return _directoryEntry.Properties["department"].Value?.ToString(); }
        }
        public string Manager
        {
            get {
                string manager = _directoryEntry.Properties["manager"].Value?.ToString();

                if (manager != null)
                {
                    manager = manager.Split(',')[0];
                    manager = manager.Split('=')[1];
                }
                else
                {
                    manager = "Not set!";
                }
                return manager;
            }
        }

        public bool Lockedout
        {
            get
            {
                return false;
                return Convert.ToBoolean(_directoryEntry.InvokeGet("IsAccountLocked"));
                //return lockouttime is null ? false : true;
            }
        }

        public ObservableCollection<string> Groups 
        {
            get
            {
                ObservableCollection<string> groups = new ObservableCollection<string>();

                object g = _directoryEntry.Invoke("Groups");
                foreach (object ob in (IEnumerable)g)
                {
                    DirectoryEntry group = new DirectoryEntry(ob);

                    string groupName = group.Name;

                    groupName = groupName.Split(',')[0];
                    groupName = groupName.Split('=')[1];

                    groups.Add(groupName);
                }

                return groups;
            }
        }

    }
}
