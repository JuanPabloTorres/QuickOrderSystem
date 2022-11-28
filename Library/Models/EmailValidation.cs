using Library.AbstractModels;
using System;

namespace Library.Models
{
    public class EmailValidation : BaseModel
    {
        public string Email { get; set; }

        public DateTime ExpDate { get; set; }

        public Guid UserId { get; set; }

        public string ValidationCode { get; set; }
    }
}