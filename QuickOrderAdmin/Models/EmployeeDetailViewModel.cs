using Library.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;

namespace QuickOrderAdmin.Models
{
    public class EmployeeDetailViewModel
    {
        public Guid EmployeeId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmpType { get; set; }


        public IList<EmployeeWorkHour> EmployeeWorkHours { get; set; }

    }
}
