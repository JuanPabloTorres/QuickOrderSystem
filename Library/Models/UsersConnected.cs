using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Models
{
    public class UsersConnected
    {

        public Guid UserID { get; set; }

        [Key]
        public string HubConnectionID { get; set; }
    }
}
