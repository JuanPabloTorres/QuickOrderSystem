namespace Library.DTO
{
    public class StripePaymentCardResult
    {
        private string errorMsg;

        private bool hasError;

        public StripePaymentCardResult ()
        {
        }

        public StripePaymentCardResult (string tokenid)
        {
            this.TokenId = tokenid;

            this.HasError = false;

            this.ErrorMsg = string.Empty;
        }

        public StripePaymentCardResult (bool _haserror, string _errormsg)
        {
            this.HasError = _haserror;

            this.ErrorMsg = _errormsg;
        }

        public string ErrorMsg
        {
            get { return errorMsg; }

            set { errorMsg = value; }
        }

        public bool HasError
        {
            get { return hasError; }

            set { hasError = value; }
        }

        public string TokenId { get; set; }
    }
}