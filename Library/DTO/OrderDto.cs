using System;
using Library.Models;

namespace Library.DTO
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Status OrderStatus { get; set; }
        public string StoreName { get; set; }
        public byte[] StoreImage { get; set; }
        public int ProductQuantity { get; set; }
        public double OrderTotal { get; set; }
        public Models.Type OrderType { get; set; }

        public Guid StoreId { get; set; }

        public OrderDto()
        {
        }
    }
}
