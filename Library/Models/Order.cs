using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public enum Status
    {
        InProccess,
        Pending,
        Completed,
        OnTheWay,
        NotSubmited,
        Submited,
    }

    public enum Type
    {
        PickUp,
        Delivery,
        None
    }

    [Table("Orders")]
    public class Order : BaseModel
    {
        public Guid BuyerId { get; set; }

        public DateTime OrderDate { get; set; }

        [Key]
        public Guid OrderId { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public Status OrderStatus { get; set; }

        public Type OrderType { get; set; }

        public Employee PrepareBy { get; set; }

        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store StoreOrder { get; set; }

        private bool IsSubmit { get; set; }
    }
}