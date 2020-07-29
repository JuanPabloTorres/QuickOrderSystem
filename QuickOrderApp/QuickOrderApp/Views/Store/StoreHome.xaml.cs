using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreHome : ContentPage
    {
        public StoreHome()
        {
            InitializeComponent();
            BindingContext = new StoreViewModel();
        }
    }
}