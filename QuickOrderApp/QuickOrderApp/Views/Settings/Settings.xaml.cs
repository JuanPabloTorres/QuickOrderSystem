using QuickOrderApp.ViewModels.SettingVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        SettingViewModel SettingViewModel;
        public Settings()
        {
            InitializeComponent();
            BindingContext = SettingViewModel = new SettingViewModel();
        }
    }
}