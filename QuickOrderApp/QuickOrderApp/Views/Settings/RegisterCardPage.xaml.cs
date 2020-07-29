using QuickOrderApp.ViewModels.SettingVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCardPage : ContentPage
    {
        public RegisterCardPage()
        {
            InitializeComponent();
            BindingContext = new RegisterCardViewModel();
        }
    }
}