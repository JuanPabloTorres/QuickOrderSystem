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

           await HomeViewModel.LoadItems();

            
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
        private async void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var collectionlastindex = e.LastVisibleItemIndex;

            var i = HomeViewModel.Stores.Count - 1;

            if (collectionlastindex == i)
            {

                await HomeViewModel.LoadDifferentItems();
            }


            //await Shell.Current.DisplayAlert("Notification", collectionlastindex.ToString() + "Found", "OK");

        }

      
    }
}