using QuickOrderApp.ViewModels.LoginVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountVerification : ContentPage
    {
        public AccountVerification()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<VerifyAccountViewModel>();
        }
    }
}