using Library.Helpers;

namespace Library.ApiResponses
{
    public class Response<T> where T : class
    {
        public string Message { get; set; }

        public T Data { get; set; }

        public bool IsValid { get; set; }

        public ResponseStatus Status { get; set; }
    }
}