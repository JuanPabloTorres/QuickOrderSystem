using QuickOrderApp.ViewModels.LoginVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
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