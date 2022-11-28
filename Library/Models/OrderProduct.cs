using Library.AbstractModels;
using Library.Helpers;
using System;

namespace Library.Models
{
    public class OrderProduct:BaseModel
    {
        public Guid BuyerId { get; set; }
   
        public Guid OrderProductId { get; set; }

        public double Price { get; set; }
        public Guid ProductIdReference { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
   
        public ProductType Type { get; set; }
    }
}