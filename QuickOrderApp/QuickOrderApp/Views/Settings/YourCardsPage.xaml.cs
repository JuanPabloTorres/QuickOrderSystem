using QuickOrderApp.ViewModels.SettingVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourCardsPage : ContentPage
    {
        public static string Route = "YourCardRoute";
        public YourCardsPage()
        {
            InitializeComponent();
            BindingContext = new UserCardsViewModel();
        }
    }
}