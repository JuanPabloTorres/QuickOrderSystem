using QuickOrderApp.ViewModels.OrderVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrderDetailPage : ContentPage
    {
        public StoreOrderDetailPage()
        {
            InitializeComponent();
            BindingContext = new OrderDetailViewModel();
        }
    }
}