using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreEmployeesPage : ContentPage
    {
        StoreEmployeeViewModel viewModel;
        public StoreEmployeesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StoreEmployeeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetEmployeeDataOfStore(StoreControlPanelViewModel.YourSelectedStore.ID.ToString());


        }
    }
}