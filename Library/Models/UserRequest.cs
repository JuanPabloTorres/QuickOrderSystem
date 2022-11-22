using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public enum Answer
    {
        None,
        Accept,
        Decline
    }

    public enum RequestType
    {
        JobRequest
    }

    public class UserRequest
    {
        public Guid FromStore { get; set; }

        public Answer RequestAnswer { get; set; }

        [Key]
        public Guid RequestId { get; set; }

        public Guid ToUser { get; set; }
        public RequestType Type { get; set; }
    }
}