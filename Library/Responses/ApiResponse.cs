using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Responses
{
    public class ApiResponse<T> where T :class
    {
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
