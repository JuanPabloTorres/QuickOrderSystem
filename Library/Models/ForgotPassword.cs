using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ForgotPassword
    {
        [Key]
        public string Code { get; set; }

        public string Email { get; set; }
    }
}