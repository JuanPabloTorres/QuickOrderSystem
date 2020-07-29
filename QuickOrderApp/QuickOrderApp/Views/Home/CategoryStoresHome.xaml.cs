using QuickOrderApp.ViewModels.HomeVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryStoresHome : ContentPage
    {

        public static string Route = "CategoryStoreRoute";
        public CategoryStoresHome()
        {
            InitializeComponent();

            BindingContext = new CategoryStoresViewModel();
        }
    }
}