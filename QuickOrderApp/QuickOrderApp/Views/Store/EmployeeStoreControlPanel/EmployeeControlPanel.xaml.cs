using QuickOrderApp.ViewModels.StoreAndEmployeesVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.EmployeeStoreControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeControlPanel : ContentPage
    {
        public EmployeeControlPanel()
        {
            InitializeComponent();
            BindingContext = new EmployeeControlHomeViewModel();


            NavigationPage.SetHasBackButton(this, false);
        }


        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            base.OnBackButtonPressed();




            // If you want to stop the back button
            return false;

        }
    }
}