using Library.Exceptions.CustomException;
using System.Net;

namespace Library.Exceptions
{
    public class NotFoundCustomException : CustomHttpException
    {
        public NotFoundCustomException(string message) : base(HttpStatusCode.NotFound, message) { }
    }
}
