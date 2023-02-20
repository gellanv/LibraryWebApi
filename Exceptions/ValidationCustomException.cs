using Library.Exceptions.CustomException;
using System.Net;

namespace Library.Exceptions
{
    public class ValidationCustomException : CustomHttpException
    {
        public ValidationCustomException(string message) : base(HttpStatusCode.BadRequest, message) { }
    }
}
