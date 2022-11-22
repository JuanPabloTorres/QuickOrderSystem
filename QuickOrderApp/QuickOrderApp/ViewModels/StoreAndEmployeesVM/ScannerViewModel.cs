using Library.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.StoreAndEmployeesVM
{
    [QueryProperty("OrderScannedId", "Id")]
    public class ScannerViewModel : BaseViewModel
    {
        private string orderIdScanned;

        private Order orderScanned;

        private string orderscannedId;

        public ScannerViewModel ()
        {
            MessagingCenter.Subscribe<Order>(this, "orderscanned", (sender) =>
            {
                OrderScanned = sender;
            });
        }

        public string OrderIdScanned
        {
            get { return orderIdScanned; }
            set
            {
                orderIdScanned = value;

                OnPropertyChanged();
            }
        }

        public Order OrderScanned
        {
            get { return orderScanned; }
            set
            {
                orderScanned = value;

                OnPropertyChanged();
            }
        }

        public string OrderScannedId
        {
            get { return orderscannedId; }
            set
            {
                orderscannedId = value;

                OnPropertyChanged();

                orderDataStore.GetItemAsync(OrderScannedId);
            }
        }

        public ICommand ScanndCommad => new Command(() =>
         {
         });
    }
}