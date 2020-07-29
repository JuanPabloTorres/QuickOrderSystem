using Library.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.ViewModels.OrderVM
{
    [QueryProperty("OrderId", "Id")]
    public class OrderDetailViewModel : BaseViewModel
    {
        private Order detailOrder;

        public Order DetailOrder
        {
            get { return detailOrder; }
            set
            {
                detailOrder = value;
                OnPropertyChanged();
            }
        }

        private string orderId;

        public string OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                OnPropertyChanged();
            }
        }

        public ICommand CompleteOrderCommand { get; set; }

        public OrderDetailViewModel()
        {

            CompleteOrderCommand = new Command(async () =>
            {
                if (DetailOrder.OrderStatus != Status.Completed)
                {

                    DetailOrder.OrderStatus = Status.Completed;

                    var orderStatusUpdateResult = await orderDataStore.UpdateItemAsync(DetailOrder);

                    if (orderStatusUpdateResult)
                    {
                        await Shell.Current.DisplayAlert("Notification", "Order Update", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Order Completed", "OK");
                }
            });

            MessagingCenter.Subscribe<Order>(this, "Detail", (sender) =>
            {
                DetailOrder = sender;
            });

            //MessagingCenter.Subscribe<Order>(null,"Detail",(sender,arg))
        }

    }
}
