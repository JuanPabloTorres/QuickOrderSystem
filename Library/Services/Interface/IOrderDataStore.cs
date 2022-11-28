using Library.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Interface
{
    public interface IOrderDataStore : IDataStore<Order>
    {
        Task<bool> DisableOrder (Guid orderId);

        Task<IEnumerable<Order>> GetOrdersOfStoreOfUserWithSpecifiStatus (Guid userid, Guid storeid, Status status);

        Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatus (Guid userid, Status status, string token);

        Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatusDifferent (IEnumerable<Order> ordersAdded, Status status, Guid userid);

        Task<IEnumerable<Order>> GetStoreOrders (Guid storeId, string token);

        IEnumerable<Order> GetUserOrders (Guid userid);

        IEnumerable<Order> GetUserOrdersOfStore (Guid userid, Guid storeid);

        IEnumerable<Order> GetUserOrdersWithToken (Guid userid, string token);

        Order HaveOrderOfSpecificStore (Guid userid, Guid storeid, string token);
    }
}