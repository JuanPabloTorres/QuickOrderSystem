using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public enum EmployeeType
    {
        New,
        OrderPreperarer,
        Delivery,
        PickUpRecepcionist,
        SubAdministrator
    }

    public class Employee
    {
        public Guid EmployeeId { get; set; }

        [ForeignKey("StoreId")]
        public Store EmployeeStore { get; set; }

        [ForeignKey("UserId")]
        public User EmployeeUser { get; set; }

        public ICollection<EmployeeWorkHour> EmployeeWorkHours { get; set; }

        public Guid StoreId { get; set; }

        public EmployeeType Type { get; set; }

        public Guid UserId { get; set; }
    }
}