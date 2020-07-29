using QuickOrderApp.ViewModels.SettingVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfile : ContentPage
    {
        public UpdateProfile()
        {
            InitializeComponent();
            BindingContext = new SettingViewModel();
        }
    }
}