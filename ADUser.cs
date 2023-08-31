using System.DirectoryServices;

namespace User_Administration_Dashboard
{
    class ADUser
    {
        private DirectoryEntry _directoryEntry;

        public ADUser(DirectoryEntry directoryEntry)
        {
            _directoryEntry = directoryEntry;
        }

        public string firstName
        {
            get { return _directoryEntry.Properties["givenname"].Value?.ToString(); }
        }
        public string lastName
        {
            get { return _directoryEntry.Properties["sn"].Value?.ToString(); }
        }

        public string emailAddress
        {
            get { return _directoryEntry.Properties["mail"].Value?.ToString(); }
        }

        public string city
        {
            get { return _directoryEntry.Properties["l"].Value?.ToString(); }
        }

        public string state
        {
            get { return _directoryEntry.Properties["st"].Value?.ToString(); }
        }

        public string title
        {
            get { return _directoryEntry.Properties["title"].Value?.ToString(); }
        }
        public string department
        {
            get { return _directoryEntry.Properties["department"].Value?.ToString(); }
        }
        public string manager
        {
            get {
                string manager = _directoryEntry.Properties["manager"].Value?.ToString();
                manager = manager.Split(',')[0];
                manager = manager.Split('=')[1];
                return manager;
            }
        }

        public bool lockedout
        {
            get
            {
                string lockouttime = _directoryEntry.Properties["lockouttime"].Value?.ToString();

                return lockouttime is null ? false : true;
            }
        }
    }
}
