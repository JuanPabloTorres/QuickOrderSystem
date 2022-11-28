using Library.ApiResponses;
using Library.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Factories
{
    public  class ResponseFactory<T> where T : class
    {

        public Response<T> FailResponse(string failMessage)
        {
            Response<T> _failResponse = new Response<T>()
            {
                Message = failMessage,
                Status = ResponseStatus.Error,
                Data = null
            };

            return _failResponse;
        }
        
        public Response<T> SuccessResponse(string successMessage)
        {
            Response<T> _failResponse = new Response<T>()
            {
                Message = successMessage,
                Status = ResponseStatus.Success,
                Data = null
            };

            return _failResponse;
        }
        
        public Response<T> ApiRequestFail()
        {
            Response<T> _requestFailResponse = new Response<T>()
            {
                Message = "Request Fail",
                Status = ResponseStatus.RequestFail
            };

            return _requestFailResponse;
        }

        public Response<T> DataResponse(string message,T data)
        {
            Response<T> _dataResponse = new Response<T>()
            {
                Message = message,
                Status = ResponseStatus.Success,
                Data = data
            };

            return _dataResponse;
        }

        public Response<T> ResponseSpecificStatus(string message , ResponseStatus status)
        {
            Response<T> _statusResponse = new Response<T>()
            {
                Message = message,
                Status = status,
                IsValid = true
            };

            return _statusResponse;
        }
    }
}
