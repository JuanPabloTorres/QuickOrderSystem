using QuickOrderApp.ViewModels.InboxVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Inbox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InboxPage : ContentPage
    {
        InboxViewModel InboxViewModel;
        public InboxPage()
        {
            InitializeComponent();
            BindingContext = InboxViewModel = new InboxViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InboxViewModel.ExecuteLoadItemsCommand();
        }
    }
}