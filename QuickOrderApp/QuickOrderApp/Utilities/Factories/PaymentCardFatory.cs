using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOrderApp.Utilities.Factories
{
    public class PaymentCardFatory
    {

        public static PaymentCard CreatePaymentcard(Guid paymentcardId, string holdername, string cardnumber, Guid userid, string cvc, string expmonth, string expyear, string stripecardId)
        {
            return new PaymentCard(paymentcardId,  holdername,  cardnumber,  userid,  cvc,  expmonth,  expyear,  stripecardId);
        }
    }
}
