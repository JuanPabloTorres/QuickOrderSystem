using Library.Models;
using System.Collections.ObjectModel;

namespace QuickOrderApp.Utilities.Grouping
{
    public class OrdersGrouping : ObservableCollection<Order>
    {
        public string Store { get; set; }
        public ObservableCollection<Order> orders { get; set; }
    }


}
