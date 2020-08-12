using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickOrderApp.Views.Store.StoreManger
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPage : ContentPage
    {
        public static string Route = "EditProductRoute";
        public EditProductPage()
        {
            InitializeComponent();

            BindingContext = new EditProductViewModel();
        }
    }
}