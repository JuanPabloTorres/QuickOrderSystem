using QuickOrderApp.ViewModels.LoginVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static string Route = "LoginRoute";
        public LoginPage()
        {
            InitializeComponent();
<<<<<<< Updated upstream
            BindingContext = new LoginViewModel();
=======
            BindingContext = new LoginViewModel(/*Navigation*/);
>>>>>>> Stashed changes
        }
    }
}