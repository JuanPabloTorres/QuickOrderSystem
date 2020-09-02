
using System;


namespace QuickOrderApp.Utilities.Dependency
{
    public class NotificationEventArgs: EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
