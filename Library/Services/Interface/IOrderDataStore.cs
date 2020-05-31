using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
    public interface IOrderDataStore:IDataStore<Order>
    {
        Order HaveOrder(Guid userid, Guid storeid);
        IEnumerable<Order> GetUserOrders(Guid userid);

        IEnumerable<Order> GetStoreOrders(Guid storeId);
    }
}
