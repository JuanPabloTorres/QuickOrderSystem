using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class UsersConnected
    {
        public DateTime ConnecteDate { get; set; }

        [Key]
        public string HubConnectionID { get; set; }

        public bool IsDisable { get; set; }

        public Guid UserID { get; set; }
    }
}