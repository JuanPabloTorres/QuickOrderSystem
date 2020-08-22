using QuickOrderApp.ViewModels.OrderVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserOrdersWithStatus : ContentPage
    {
        public static string Route = "UserOrdersRoute";

        OrderViewModel OrderViewModel;
        public UserOrdersWithStatus()
        {
            InitializeComponent();

            BindingContext= OrderViewModel = new OrderViewModel();
        }

        protected async  override void OnAppearing()
        {
            base.OnAppearing();

            //OrderViewModel.SetOrders();
        }
    }
}