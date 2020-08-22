using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
	public class AuthCode
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime EndAt { get; set; }
		public bool IsAlive { get; set; }
	}
}
