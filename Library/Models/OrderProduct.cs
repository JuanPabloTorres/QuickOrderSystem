﻿using System;

namespace Library.Models
{
    public class OrderProduct
    {
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderProductId { get; set; }

        public double Price { get; set; }
        public Guid ProductIdReference { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public Guid StoreId { get; set; }
        public ProductType Type { get; set; }
    }
}