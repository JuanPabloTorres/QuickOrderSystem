using Library.AbstractModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("Logins")]
    public class Credential : BaseModel
    {
        public bool IsConnected { get; set; }

        public string Password { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }
    }
}