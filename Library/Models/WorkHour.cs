using Library.AbstractModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("StoresWorkHours")]
    public class WorkHour : BaseModel
    {
        public DateTime CloseTime { get; set; }

        public string Day { get; set; }

        public DateTime OpenTime { get; set; }

      
    }
}