using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("StoresWorkHours")]
    public class WorkHour
    {
        [Key]
        public Guid WorkHourId { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public Guid StoreId { get; set; }

        //[ForeignKey("StoreId")]
        //public Store WorkHourStore { get; set; }

        public string Day { get; set; }
    }
}
