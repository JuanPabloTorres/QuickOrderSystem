using System;

namespace QuickOrderApp.Utilities.Dependency
{
    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; set; }

        public string Title { get; set; }
    }
}