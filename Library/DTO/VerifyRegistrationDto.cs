using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class VerifyRegistrationDto
    {
        public string Code { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
