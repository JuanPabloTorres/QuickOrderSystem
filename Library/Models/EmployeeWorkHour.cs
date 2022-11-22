using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class EmployeeWorkHour
    {
        public DateTime CloseTime { get; set; }

        public string Day { get; set; }

        public Guid EmpId { get; set; }

        [ForeignKey("EmpId")]
        public Employee Employee { get; set; }

        public DateTime OpenTime { get; set; }

        public bool WillWork { get; set; }

        [Key]
        public Guid WorkHourId { get; set; }
    }
}