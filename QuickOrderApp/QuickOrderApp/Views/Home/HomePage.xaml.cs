using QuickOrderApp.ViewModels.HomeVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel HomeViewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = HomeViewModel = new HomeViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //MessagingCenter.Send<object>(null, "Update");
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    return false;
        //}
    }
}