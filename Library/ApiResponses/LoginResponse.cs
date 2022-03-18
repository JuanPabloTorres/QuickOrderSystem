using Library.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApiResponses
{
    public class LoginResponse : BaseResponse
    {
        public Login Login { get; set; }

        public TokenDTO Token { get; set; }

        public User UserInformation { get; set; }
    }
}
