using Library.AbstractModels;
using System;

namespace Library.Models
{
    public class PaymentCard : BaseModel
    {
        public PaymentCard()
        {
        }

        public PaymentCard(Guid paymentcardId, string holdername, string cardnumber, Guid userid, string cvc, string expmonth, string expyear, string stripecardId)
        {
            this.ID = paymentcardId;

            this.HolderName = holdername;

            this.CardNumber = cardnumber;

            this.UserId = userid;

            this.Cvc = cvc;

            this.Month = expmonth;

            this.Year = expyear;

            this.StripeCardId = stripecardId;
        }

        public string CardNumber { get; set; }

        public string Cvc { get; set; }

        public string HolderName { get; set; }

        public string Month { get; set; }

        public string StripeCardId { get; set; }

        public Guid UserId { get; set; }

        public string Year { get; set; }
    }
}