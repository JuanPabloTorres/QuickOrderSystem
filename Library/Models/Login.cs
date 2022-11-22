using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Logins")]
    public class Login
    {
        public bool IsConnected { get; set; }

        [Key]
        public Guid LoginId { get; set; }

        public string Password { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }
    }
}