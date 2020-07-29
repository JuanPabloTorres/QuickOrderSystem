using Library.Models;
using System;

namespace Library.Services.Interface
{
    public interface IOrderProductDataStore : IDataStore<OrderProduct>
    {
        bool OrderProductOfUserExistInOrder(Guid userid, string productname, Guid orderid);
        OrderProduct OrderProductOfUserExistOnOrder(string productname, Guid orderId);
    }
}
