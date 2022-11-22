using Library.Models;
using System.Collections.ObjectModel;

namespace QuickOrderApp.Utilities.Grouping
{
    public class OrdersGrouping : ObservableCollection<Order>
    {
        public ObservableCollection<Order> orders { get; set; }

        public string Store { get; set; }
    }
}