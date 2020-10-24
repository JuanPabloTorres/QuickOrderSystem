using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Utilities.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreLicenceTemplate : ContentView
    {
        public StoreLicenceTemplate(MessageInboxPresenter messageInboxData)
        {
            InitializeComponent();

            BindingContext = messageInboxData;
        }
    }
}