using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class ResponseDto
    {
        public string TextMessage { get; set; }
        public bool HasErrors { get; set; }

        public ResponseDto()
        {

        }
    }
}
