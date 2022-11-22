using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class EmailValidation
    {
        public string Email { get; set; }

        [Key]
        public Guid EmailValidationId { get; set; }

        public DateTime ExpDate { get; set; }

        public Guid UserId { get; set; }

        public string ValidationCode { get; set; }
    }
}