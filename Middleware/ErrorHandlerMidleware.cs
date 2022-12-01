﻿using System.Net;
using System.Text.Json;

namespace SchollOfDevs.Middleware
{
    public class ErrorHandlerMidleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMidleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = error switch
                {
                    BadHttpRequestException => (int)HttpStatusCode.BadRequest, //custom application error
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, // not found error
                    FormatException => (int)HttpStatusCode.Forbidden, // unauthorized
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}