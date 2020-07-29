using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class UserRequest
    {
        [Key]
        public Guid RequestId { get; set; }

        public Guid ToUser { get; set; }

        public Guid FromStore { get; set; }

        public RequestType Type { get; set; }

        public Answer RequestAnswer { get; set; }
    }

    public enum RequestType
    {
        JobRequest
    }

    public enum Answer
    {
        None,
        Accept,
        Decline
    }
}
