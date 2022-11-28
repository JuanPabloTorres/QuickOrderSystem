using Library.AbstractModels;
using System;

namespace Library.Models
{
    public class UsersConnected : BaseModel
    {
        public DateTime ConnecteDate { get; set; }

        public bool IsDisable { get; set; }

        public Guid UserID { get; set; }

        public string HubConnectionID { get; set; }
    }
}