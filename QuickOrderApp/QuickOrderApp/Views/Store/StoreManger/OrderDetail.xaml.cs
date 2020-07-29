using QuickOrderApp.ViewModels.OrderVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetail : ContentPage
    {
        public OrderDetail()
        {
            InitializeComponent();
            BindingContext = new OrderDetailViewModel();
        }
    }
}