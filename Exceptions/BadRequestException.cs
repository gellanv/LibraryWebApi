using Library.Exceptions.CustomException;
using System.Net;

namespace Library.Exceptions
{
    public class BadRequestCustomException : CustomHttpException
    {
        public BadRequestCustomException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
