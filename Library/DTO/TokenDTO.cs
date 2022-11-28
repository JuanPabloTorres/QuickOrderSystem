using Library.Models;
using System;
using System.Runtime.Serialization;

namespace Library.DTO
{
    [DataContract]
    public class TokenDTO
    {
        [DataMember]
        public DateTime Exp { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public AppUser UserDetail { get; set; }
    }
}