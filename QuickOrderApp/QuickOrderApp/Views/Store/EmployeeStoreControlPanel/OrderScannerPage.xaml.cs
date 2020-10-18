using Library.Models;
using Library.Services;
using Microsoft.Extensions.DependencyInjection;
using QuickOrderApp.ViewModels.StoreAndEmployeesVM;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace QuickOrderApp.Views.Store.EmployeeStoreControlPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderScannerPage : ContentPage
    {
        public static string Route = "ScannerRoute";

        ScannerViewModel scannerViewModel;
        public OrderScannerPage()
        {
            InitializeComponent();
            BindingContext = scannerViewModel = new ScannerViewModel();
        }

        public  void scanView_OnScanResult(Result result)
        {

            OrderDataStore orderDataStore = Startup.ServiceProvider.GetRequiredService<OrderDataStore>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                scanView.IsScanning = false;
                //await Shell.Current.DisplayAlert("Notification", result.Text, "OK");
                var order =await orderDataStore.GetItemAsync(result.Text);


                await EmployeeShell.Current.GoToAsync($"{ScannedOrder.Route}");

                MessagingCenter.Send<Order>(order, "orderscanned");
                //await Shell.Current.DisplayAlert("Scanned result", "The barcode's text is " + scannerViewModel.OrderIdScanned + ". The barcode's format is " + result.BarcodeFormat, "OK");
            });
        }
    }
}