﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public Guid BuyerId { get; set; }

        public Type OrderType { get; set; }

    }

    public enum Type
    {
        PickUp,
        Delivery
    }
}
