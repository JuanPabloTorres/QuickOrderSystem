using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Interface
{
    public interface IOrderProductDataStore:IDataStore<OrderProduct>
    {
        bool OrderProductOfUserExist(Guid userid, string productname);
        OrderProduct OrderProductOfUserExistWith(string productname);
    }
}
