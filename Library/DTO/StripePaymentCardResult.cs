using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Library.DTO
{
    public class StripePaymentCardResult
    {

		private bool hasError;

		public bool HasError
		{
			get { return hasError; }
			set { hasError = value; }
		}

		private string errorMsg;

		public string ErrorMsg
		{
			get { return errorMsg; }
			set { errorMsg = value; }
		}

		public string TokenId { get; set; }

		public StripePaymentCardResult()
		{

		}

		public StripePaymentCardResult(string tokenid)
		{
			this.TokenId = tokenid;

			this.HasError = false;
			this.ErrorMsg = string.Empty;
		}

		public StripePaymentCardResult(bool _haserror, string _errormsg)
		{
			this.HasError = _haserror;
			this.ErrorMsg = _errormsg;
		}


	}
}
