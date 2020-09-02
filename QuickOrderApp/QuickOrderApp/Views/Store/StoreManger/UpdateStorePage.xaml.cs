using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateStorePage : ContentPage
    {
        public static string Route = "UpdateStoreRoute";
        public UpdateStorePage()
        {
            InitializeComponent();
            BindingContext = new UpdateStoreViewModel();
        }
    }
}