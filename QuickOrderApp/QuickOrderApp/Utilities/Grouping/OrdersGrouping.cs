using Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace QuickOrderApp.Utilities.Grouping
{
    public class OrdersGrouping:ObservableCollection<Order>
    {
        public string Store { get; set; }
        public ObservableCollection<Order> orders { get; set; }
    }

   
}
