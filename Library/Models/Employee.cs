using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User EmployeeUser { get; set; }

        public EmployeeType Type { get; set; }

        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store EmployeeStore { get; set; }

        public ICollection<EmployeeWorkHour> EmployeeWorkHours { get; set; }
    }

    public enum EmployeeType
    {
        New,
        OrderPreperarer,
        Delivery,
        PickUpRecepcionist,
    }
}
