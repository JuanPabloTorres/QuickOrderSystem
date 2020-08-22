using Library.Models;
using QuickOrderApp.Utilities.Presenters;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

                EmployeeOrderPresenter = new EmployeeOrderPresenter(DetailOrder);
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

        private string orderStatus;

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value;
                OnPropertyChanged();
            }
        }


        public ICommand CompleteOrderCommand { get; set; }


        private EmployeeOrderPresenter employeeOrderPresenter;

        public EmployeeOrderPresenter EmployeeOrderPresenter
        {
            get { return employeeOrderPresenter; }
            set { employeeOrderPresenter = value;
                OnPropertyChanged();
            }
        }


        public OrderDetailViewModel()
        {
            
            MessagingCenter.Subscribe<EmployeeOrderPresenter>(this, "OrderDetail", (sender) =>
            {
                //DetailOrder = sender;
                EmployeeOrderPresenter = sender;
                OrderStatus = EmployeeOrderPresenter.OStatus.ToString();


            });


            CompleteOrderCommand = new Command(async () =>
            {

                if (EmployeeOrderPresenter.OrderStatus != Status.Completed)
                {
                    if (EmployeeOrderPresenter.OrderProductsPresenter.All(op => op.IsComplete == true))
                    {
                        EmployeeOrderPresenter.OrderStatus = Status.Completed;


                        var updateOrder = new Order()
                        {
                            BuyerId = EmployeeOrderPresenter.BuyerId,
                            OrderStatus = EmployeeOrderPresenter.OrderStatus,
                            OrderDate = EmployeeOrderPresenter.OrderDate,
                            OrderId = EmployeeOrderPresenter.OrderId,
                            OrderType = EmployeeOrderPresenter.OrderType,
                            StoreId = EmployeeOrderPresenter.StoreId,
                            OrderProducts = EmployeeOrderPresenter.OrderProducts


                        };

                        var orderStatusUpdateResult = await orderDataStore.UpdateItemAsync(updateOrder);

                        if (orderStatusUpdateResult)
                        {
                            OrderStatus = updateOrder.OrderStatus.ToString();

                            MessagingCenter.Send<EmployeeOrderPresenter>(EmployeeOrderPresenter, "RemoveEmpOrderPrensenter");
                            await Shell.Current.DisplayAlert("Notification", "Order Update...!", "OK");
                        }

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Notification", "Some product are not completed yet..!", "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Notification", "Order Completed..!", "OK");
                }
            });



        }




    }
}
