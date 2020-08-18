using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Logins")]
    public class Login
    {
        public Guid LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public bool IsConnected { get; set; }

        public Guid UserId { get; set; }
       
    }
}
