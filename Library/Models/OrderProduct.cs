using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{

    public class OrderProduct
    {
        public Guid OrderProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public Guid BuyerId { get; set; }

        public Guid StoreId { get; set; }

        public Guid OrderId { get; set; }

        public byte[] ProductImage { get; set; }


    }
}
