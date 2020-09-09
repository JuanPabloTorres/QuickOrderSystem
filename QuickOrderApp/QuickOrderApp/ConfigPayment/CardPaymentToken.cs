using System;
using System.Collections.Generic;
using System.Text;

namespace QuickOrderApp.ConfigPayment
{
    public class CardPaymentToken
    {
		private string cardtokenId;

		public string CardTokenId
		{
			get { return cardtokenId; }
			set { cardtokenId = value; }
		}
	}
}
