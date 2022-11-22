using Library.Models;
using System;

namespace Library.Services.Interface
{
    public interface IOrderProductDataStore : IDataStore<OrderProduct>
    {
        bool OrderProductOfUserExistInOrder (Guid userid, Guid prodcutId, Guid orderid);

        OrderProduct OrderProductOfUserExistOnOrder (Guid productId, Guid orderId);
    }
}