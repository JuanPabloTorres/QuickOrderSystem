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
    public partial class YourStoresPage : ContentPage
    {

        public static string Route = "YourStoreRoute";
        public YourStoresPage()
        {
            InitializeComponent();
            BindingContext = new YourStoresViewModel();
        }
    }
}