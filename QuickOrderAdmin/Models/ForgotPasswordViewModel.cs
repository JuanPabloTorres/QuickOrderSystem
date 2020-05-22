using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickOrderAdmin.Models
{
    public class ForgotPasswordViewModel
    {

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Please enter your e-mail address."), StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter code."), StringLength(50)]
        public string Code { get; set; }



    }
}
