using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApiResponses
{
    public class BaseResponse
    {
        public Code ResponseCode { get; set; }

        public string Message { get; set; }

        public IList<object> Data { get; set; }

        public object ObjectItem { get; set; }


    }
}
