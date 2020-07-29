
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryHomePage : ContentPage
    {
        public CategoryHomePage()
        {
            InitializeComponent();
            BindingContext = new QuickOrderApp.ViewModels.HomeVM.HomeViewModel();
        }
    }
}