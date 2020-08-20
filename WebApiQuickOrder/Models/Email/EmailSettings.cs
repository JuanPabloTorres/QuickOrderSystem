namespace WebApiQuickOrder.Models
{
	public interface IEmailSettings
	{
		public string MailServer { get; set; }
		public int Port { get; set; }
		public string SenderName { get; set; }
		public string Sender { get; set; }
		public string Password { get; set; }
	}

	public class EmailSettings : IEmailSettings
	{
		public string MailServer { get; set; }
		public int Port { get; set; }
		public string SenderName { get; set; }
		public string Sender { get; set; }
		public string Password { get; set; }
	}
}