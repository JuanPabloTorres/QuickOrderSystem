using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    
    public class EmailValidation
    {
        [Key]
        public Guid EmailValidationId { get; set; }

        public string ValidationCode { get; set; }

        public DateTime ExpDate { get; set; }

        public string Email { get; set; }

        public Guid UserId { get; set; }

    }
}
