using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickOrderAdmin.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter fullname"), StringLength(50)]
        public string Name { get; set; }


        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Please enter your e-mail address."), StringLength(50)]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your username"), StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password"), StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your confirmpassword"), StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Store Register License")]
        public Guid StoreLicence { get; set; }

        public UserViewModel()
        {
            Name = string.Empty;
            Email = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            StoreLicence = Guid.Empty;
        }
    }
}
