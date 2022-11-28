using Library.AbstractModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Licences")]
    public class StoreLicense : BaseModel
    {
        public bool IsUsed { get; set; }

        public Guid LicenseHolderUserId { get; set; }

        public DateTime StartDate { get; set; }
    }
}