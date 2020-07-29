using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
