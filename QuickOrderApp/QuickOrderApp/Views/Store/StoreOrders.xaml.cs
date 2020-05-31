using QuickOrderApp.ViewModels;
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
    public partial class StoreOrders : ContentPage
    {
        OrderViewModel orderVm;
        public StoreOrders()
        {
            InitializeComponent();
            BindingContext = orderVm = new OrderViewModel();
        }

        protected override void OnAppearing()
        {
            orderVm.ExecuteLoadItemsCommand();
        }


    }
}