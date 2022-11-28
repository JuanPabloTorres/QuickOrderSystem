using Library.AbstractModels;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Orders")]
    public class Order : BaseModel
    {
        public Guid BuyerId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public Status OrderStatus { get; set; }

        public Helpers.Type OrderType { get; set; }

        public Employee PrepareBy { get; set; }

        [ForeignKey("StoreId")]
        public Store StoreOrder { get; set; }

        private bool IsSubmit { get; set; }
    }
}