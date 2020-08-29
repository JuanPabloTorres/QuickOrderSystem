using System;

namespace Library.Models
{
    public class AuthCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsAlive { get; set; }
        public string Email { get; set; }
    }
}