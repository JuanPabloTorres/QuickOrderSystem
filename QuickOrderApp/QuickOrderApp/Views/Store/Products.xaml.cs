using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentPage
    {
        StoreViewModel StoreViewModel;

        public Products()
        {
            InitializeComponent();

            BindingContext = StoreViewModel = new StoreViewModel();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await StoreViewModel.GetStoreInformation(App.CurrentStore.StoreId.ToString());

        }


    }
}
