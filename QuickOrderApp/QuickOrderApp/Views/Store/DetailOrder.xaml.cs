using Microsoft.Extensions.DependencyInjection;
using QuickOrderApp.ViewModels.OrderVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailOrder : ContentPage
    {
        public static string LoadOrderDetail = nameof(LoadOrderDetail);
        public DetailOrder()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetRequiredService<UserDetailOrderViewModel>();
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Send(this,LoadOrderDetail);
            base.OnAppearing();
        }
    }
}