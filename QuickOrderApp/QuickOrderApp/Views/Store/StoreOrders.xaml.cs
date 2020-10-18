using Microsoft.Extensions.DependencyInjection;
using QuickOrderApp.ViewModels.OrderVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreOrders : ContentPage
    {
        OrderViewModel orderVm;
        public StoreOrders()
        {
            InitializeComponent();
            BindingContext = orderVm = Startup.ServiceProvider.GetRequiredService<OrderViewModel>();
        }

        //protected override void OnAppearing()
        //{
        //    orderVm.ExecuteLoadItemsCommand();
        //}


    }
}