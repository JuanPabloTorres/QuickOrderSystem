using Library.AbstractModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Subcriptions")]
    public class Subcription:BaseModel
    {
        public bool IsDisable { get; set; }

        public string Status { get; set; }

        public Guid StoreLicense { get; set; }

        public string StripeCustomerId { get; set; }

        public string StripeSubcriptionID { get; set; }


    }
}