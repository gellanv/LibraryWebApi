using Library.Exceptions.CustomException;
using Newtonsoft.Json;
using System.Net;

namespace Library.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (CustomHttpException customExeption)
            {
                await HandleExceptionAsync(context, customExeption);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }     

        private static Task HandleExceptionAsync(HttpContext context, CustomHttpException ex)
        {
            var errorMessage = JsonConvert.SerializeObject(new { Message = ex.Message, Code = ex.StatusCode });
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ex.StatusCode;
          
            return context.Response.WriteAsync(errorMessage);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception tempExeption)
        {
            CustomHttpException tempCuspomExeption = new(HttpStatusCode.BadRequest, tempExeption.Message);

            var errorMessage = JsonConvert.SerializeObject(new { Message = tempExeption.Message, Code = HttpStatusCode.BadRequest });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
