using Newtonsoft.Json;
using System.Net;

namespace Library.Exceptions.CustomException
{
    public class CustomHttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public CustomHttpException(HttpStatusCode statusCode, string Message) : base(Message)
        {
            StatusCode = statusCode;
        }
    }
}