using QuickOrderApp.ViewModels.SettingVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInformation : ContentPage
    {
        SettingViewModel SettingViewModel;
        public UserInformation()
        {
            InitializeComponent();
            BindingContext = SettingViewModel = new SettingViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = SettingViewModel = new SettingViewModel();
        }
    }
}