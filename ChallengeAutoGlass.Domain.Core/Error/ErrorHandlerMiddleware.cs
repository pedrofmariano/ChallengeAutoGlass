using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChallengeAutoGlass.Domain.Core.Error
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                (int statusCode, object responseBody) = DecodeException(context, ex);
            }
        }

        private (int, object) DecodeException(HttpContext context, Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var responseBody = new
            {
                code = "internal_server_error",
                message = "Internal server error"
            };

            if (ex is BaseException)
            {
                var err = (BaseException)ex;
                responseBody = new
                {
                    code = err.Code,
                    message = err.Message
                };
            }

            switch (ex)
            {
                case BadRequestException e:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException e:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ConflictException e:
                    statusCode = (int)HttpStatusCode.Conflict;
                    break;
                case InternalServerErrorException e:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return (statusCode, responseBody);
        }
    }
}
