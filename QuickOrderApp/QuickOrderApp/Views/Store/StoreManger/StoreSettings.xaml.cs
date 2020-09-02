using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreSettings : ContentPage
    {

    
        public static string Route = "StoreSettingRoute";
        public StoreSettings()
        {
            InitializeComponent();
            BindingContext = new StoreSettingViewModel();
        }
    }
}