using Library.Models;
using QuickOrderApp.Views.Store.StoreManger;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Presenters
{
    public class StoreOrderPresenter
    {

        private Order detailOrder;

        public Order DetailOrder
        {
            get { return detailOrder; }
            set { detailOrder = value; }
        }

        public ICommand DetailCommand { get; set; }

        public ICommand DetailEmployeeOrderCommand { get; set; }

        public StoreOrderPresenter(Order order)
        {

            DetailOrder = order;

            DetailCommand = new Command(async () =>
            {

                try
                {
                    await Shell.Current.GoToAsync($"StoreDetailOrderRoute", animate: true);
                    MessagingCenter.Send<Order>(DetailOrder, "Detail");
                    //await Shell.Current.GoToAsync($"StoreDetailOrderRoute?Id={DetailOrder.StoreId.ToString()}", animate: true);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
                //await Shell.Current.GoToAsync("OrderDetailRoute",true);
            });

            DetailEmployeeOrderCommand = new Command(async () =>
            {

                try
                {
                    await EmployeeShell.Current.GoToAsync($"{StoreOrderDetailPage.Route}", animate: true);

                    EmployeeOrderPresenter employeeOrderPresenter = new EmployeeOrderPresenter(order);

                    MessagingCenter.Send<EmployeeOrderPresenter>(employeeOrderPresenter, "OrderDetail");
                   
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
              
            });

           
        }

    }
}
