using Library.AbstractModels;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Employee:BaseModel
    {
        [ForeignKey("StoreId")]
        public Store EmployeeStore { get; set; }

        public ICollection<EmployeeWorkHour> EmployeeWorkHours { get; set; }

        public Guid StoreId { get; set; }

        public EmployeeType Type { get; set; }

        public Guid UserId { get; set; }
        public AppUser EmployeeUser { get; set; }
    }
}