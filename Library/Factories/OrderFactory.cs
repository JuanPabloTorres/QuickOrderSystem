using System;
using System.Collections.Generic;
using System.Linq;
using Library.DTO;
using Library.Models;

namespace Library.Factories
{
    public interface IOrderFactory
    {
        OrderDto CreateSimpleOrderDto(Order order, Store store, List<OrderProduct> orderProducts);
        OrderDto CreateSimpleOrderDto(Order order, byte[] storeImage, string storeName);
    }
    public class OrderFactory : IOrderFactory
    {
        public OrderFactory()
        {
        }

        public OrderDto CreateSimpleOrderDto(Order order, Store store, List<OrderProduct> orderProducts)
        {
            return new OrderDto {
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus,
                StoreImage = store.StoreImage,
                StoreName = store.StoreName,
                OrderTotal = orderProducts.Sum(op=>op.Price),
                ProductQuantity = orderProducts.Count
            };
        }

        public OrderDto CreateSimpleOrderDto(Order order, byte[] storeImage, string storeName)
        {
            return new OrderDto
            {
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus,
                StoreImage = storeImage,
                StoreName = storeName
            };
        }
    }
}
