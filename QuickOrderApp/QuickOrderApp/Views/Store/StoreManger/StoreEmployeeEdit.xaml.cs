using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreEmployeeEdit : ContentPage
    {
        public StoreEmployeeEdit()
        {
            InitializeComponent();
            BindingContext = new StoreEmployeeEditViewModel();
        }
    }
}