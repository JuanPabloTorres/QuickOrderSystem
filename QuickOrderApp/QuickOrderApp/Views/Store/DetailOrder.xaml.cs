using QuickOrderApp.ViewModels.OrderVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailOrder : ContentPage
    {
        public DetailOrder()
        {
            InitializeComponent();
            BindingContext = new UserDetailOrderViewModel();
        }
    }
}