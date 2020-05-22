using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
