using System.ComponentModel.DataAnnotations;

namespace QuickOrderAdmin.Models
{
    public class CardViewModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string cardNumber { get; set; }

        [Required]
        public int MM { get; set; }

        [Required]
        public int YY { get; set; }

        [Required]
        public int CVV { get; set; }
    }
}
