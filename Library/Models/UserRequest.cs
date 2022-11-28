using Library.AbstractModels;
using Library.Helpers;
using System;

namespace Library.Models
{
    public class UserRequest : BaseModel
    {
        public Guid FromStore { get; set; }

        public Answer RequestAnswer { get; set; }

        public Guid ToUser { get; set; }
        public RequestType Type { get; set; }
    }
}