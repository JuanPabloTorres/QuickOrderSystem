using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Models
{
    public class ForgotPassword
    {
        [Key]
        public string Code { get; set; }

        public string Email { get; set; }



    }
}
