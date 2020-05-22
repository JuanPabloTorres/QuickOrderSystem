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

    }
}
