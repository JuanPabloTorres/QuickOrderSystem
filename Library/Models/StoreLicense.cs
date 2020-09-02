using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Licences")]
    public class StoreLicense
    {
        [Key]
        public Guid LicenseId { get; set; }
        public DateTime StartDate { get; set; }
        public Guid LicenseHolderUserId { get; set; }

        public bool IsUsed { get; set; }
    }
}
