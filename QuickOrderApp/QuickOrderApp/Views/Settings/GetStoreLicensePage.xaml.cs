using QuickOrderApp.ViewModels.SettingVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetStoreLicensePage : ContentPage
    {
        public GetStoreLicensePage()
        {
            InitializeComponent();
            BindingContext = new GetLicenceViewModel();
        }
    }
}