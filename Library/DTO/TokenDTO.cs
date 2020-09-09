using Library.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Library.DTO
{
    [DataContract]
    public class TokenDTO
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]

      
        public DateTime Exp { get; set; }
        [DataMember]
        public User UserDetail { get; set; }
    }





}
