using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Subcriptions")]
    public class Subcription
    {
        [Key]
        public string StripeSubCriptionID { get; set; }
        public string StripeCustomerId { get; set; }

        public string Status { get; set; }

        public bool IsDisable { get; set; }

        public Guid StoreLicense { get; set; }


    }
}
