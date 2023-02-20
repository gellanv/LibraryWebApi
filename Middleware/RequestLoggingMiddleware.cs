﻿namespace LibraryWebApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} Headers:{headers}, QueryString: {query}, Body: {body} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.Headers,
                    context.Request?.QueryString,
                    context.Request?.Body,
                    context.Response?.StatusCode
                    );
            }
        }
    }
}
