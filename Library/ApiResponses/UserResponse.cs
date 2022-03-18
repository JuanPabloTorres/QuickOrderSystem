using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApiResponses
{
    public class UserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}
