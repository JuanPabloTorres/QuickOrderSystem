using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class PaymentCardDTO
    {

       
        public Guid PaymentCardId { get; set; }
        public string HolderName { get; set; }
        public string CardNumber { get; set; }

        public Guid UserId { get; set; }

        public string Cvc { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

        public string StripeCardId { get; set; }
    }
}
