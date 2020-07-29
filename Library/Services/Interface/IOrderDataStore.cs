using Library.Models;
using System;
using System.Collections.Generic;

namespace Library.Services.Interface
{
    public interface IOrderDataStore : IDataStore<Order>
    {
        Order HaveOrderOfSpecificStore(Guid userid, Guid storeid);
        IEnumerable<Order> GetUserOrders(Guid userid);

        IEnumerable<Order> GetStoreOrders(Guid storeId);

        IEnumerable<Order> GetUserOrdersWithToken(Guid userid, string token);
    }
}
