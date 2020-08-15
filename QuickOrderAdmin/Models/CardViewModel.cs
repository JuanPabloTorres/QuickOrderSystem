using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickOrderAdmin.Models
{
    public class CardViewModel
    {
     
        [DisplayName("Holdername")]
        [Required(ErrorMessage = "Please enter your Holdername"), StringLength(50)]
        public string Holdername { get; set; }
      
        [DisplayName("Cardnumber")]
        [Required(ErrorMessage = "Please enter card number."), StringLength(16)]
       
        public string cardNumber { get; set; }

        [Required(ErrorMessage = "Please enter your month"), StringLength(2)]
        [DisplayName("Exp MM")]
        public string MM { get; set; }

        [Required(ErrorMessage = "Please enter your year"), StringLength(2)]
        [DisplayName("Exp YY")]
        public string YY { get; set; }

        [Required(ErrorMessage = "Please enter your CVC"), StringLength(3)]
        [DisplayName("CVC")]
        public string CVc { get; set; }

        [Required(ErrorMessage = "Please enter your email"), StringLength(50)]
        [DisplayName("Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }


        public CardViewModel()
        {
            Holdername = string.Empty;
            cardNumber = string.Empty;       
           
            Email = string.Empty;
        }
    }
}
