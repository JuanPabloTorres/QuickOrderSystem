using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IOrderDataStore : IDataStore<Order>
    {
        Order HaveOrderOfSpecificStore(Guid userid, Guid storeid,string token);
        IEnumerable<Order> GetUserOrders(Guid userid);

        IEnumerable<Order> GetStoreOrders(Guid storeId,string token);

        Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatus(Guid userid, Status status);

        IEnumerable<Order> GetUserOrdersWithToken(Guid userid, string token);
        IEnumerable<Order> GetUserOrdersOfStore(Guid userid, Guid storeid);

        Task<IEnumerable<Order>> GetOrdersOfStoreOfUserWithSpecifiStatus(Guid userid, Guid storeid, Status status);
    }
}
