using Library.Exceptions.CustomException;
using System.Net;

namespace LibraryWebApi.Exceptions
{
    public class InvalidSecretKeyException : CustomHttpException
    {
        public InvalidSecretKeyException(string message) : base(HttpStatusCode.Forbidden, message) { }
    }
}
