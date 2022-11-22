using QuickOrderApp.ViewModels;
using System;

namespace QuickOrderApp.Utilities.Presenters
{
    public class WorkHourPresenter : BaseViewModel
    {
        private TimeSpan close;

        private string day;

        private TimeSpan open;

        private bool willWork;

        public WorkHourPresenter (string day)
        {
            Day = day;

            Open = DateTime.Parse("8:00 AM").TimeOfDay;

            Close = DateTime.Parse("3:00 PM").TimeOfDay;
        }

        public TimeSpan Close
        {
            get { return close; }
            set
            {
                close = value;

                OnPropertyChanged();
            }
        }

        public string Day
        {
            get { return day; }
            set
            {
                day = value;

                OnPropertyChanged();
            }
        }

        public TimeSpan Open
        {
            get { return open; }
            set
            {
                open = value;

                OnPropertyChanged();
            }
        }

        public bool WillWork
        {
            get { return willWork; }
            set
            {
                willWork = value;

                OnPropertyChanged();
            }
        }
    }
}