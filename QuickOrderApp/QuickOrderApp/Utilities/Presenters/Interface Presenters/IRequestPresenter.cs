using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOrderApp.Utilities.Presenters.Interface_Presenters
{
    public interface IRequestPresenter
    {
         string Title { get; set; }

         string Message { get; set; }

        string From { get; set; }

    }
}
