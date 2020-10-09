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

        Task<IEnumerable<Order>> GetStoreOrders(Guid storeId,string token);

        Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatus(Guid userid, Status status,string token);

        IEnumerable<Order> GetUserOrdersWithToken(Guid userid, string token);
        IEnumerable<Order> GetUserOrdersOfStore(Guid userid, Guid storeid);

        Task<IEnumerable<Order>> GetOrdersOfStoreOfUserWithSpecifiStatus(Guid userid, Guid storeid, Status status);


        Task<IEnumerable<Order>> GetOrdersOfUserWithSpecificStatusDifferent(IEnumerable<Order> ordersAdded,Status status,Guid userid);

        Task<bool> DisableOrder(Guid orderId);

        Task<IEnumerable<Order>> GetDifferentStoreOrders(IEnumerable<Order> orders,Guid storeId);
    }
}
