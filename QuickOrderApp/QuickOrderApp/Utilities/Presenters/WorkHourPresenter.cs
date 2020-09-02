using Library.Models;
using QuickOrderApp.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickOrderApp.Utilities.Presenters
{
    public class WorkHourPresenter : BaseViewModel
    {
        private string day;

        public string Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan open;

        public TimeSpan Open
        {
            get { return open; }
            set
            {
                open = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan close;

        public TimeSpan Close
        {
            get { return close; }
            set
            {
                close = value;
                OnPropertyChanged();
            }
        }

        private bool willWork;

        public bool WillWork
        {
            get { return willWork; }
            set { willWork = value;
                OnPropertyChanged();
            }
        }


        public WorkHourPresenter(string day)
        {
            Day = day;

            Open = DateTime.Parse("8:00 AM").TimeOfDay;
            Close = DateTime.Parse("3:00 PM").TimeOfDay;

        }

       



    }
}
