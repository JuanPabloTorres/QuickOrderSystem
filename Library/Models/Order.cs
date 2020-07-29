using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store StoreOrder { get; set; }

        public Type OrderType { get; set; }
        public Status OrderStatus { get; set; }

        public Employee PrepareBy { get; set; }

        bool IsSubmit { get; set; }

    }

    public enum Type
    {
        PickUp,
        Delivery,
        None
    }

    public enum Status
    {
        InProccess,
        Pending,
        Completed,
        OnTheWay,
        NotSubmited,
        Submited

    }



}
