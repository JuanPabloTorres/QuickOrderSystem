using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("StoreLicences")]
    public class StoreLicense
    {
        [Key]
        public Guid LicenseId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
