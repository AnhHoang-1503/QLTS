using MISA.WebFresher05.Domain;
using System.Diagnostics;
using MISA.WebFresher05.Domain.Resources;

namespace MISA.WebFresher05
{
    /// <summary>
    /// Middleware xử lý exception
    /// </summary>
    /// Created by: vtahoang (22/07/2023) 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Xử lý các exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        /// Created by: vtahoang (22/07/2023) 
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                // Không tìm thấy
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToString() ?? "");
                    break;
                // Trùng mã
                case ConflictException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((ConflictException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToString() ?? "");
                    break;
                // Dữ liệu không hợp lệ
                case Domain.InvalidDataException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((Domain.InvalidDataException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToString() ?? "");
                    break;
                case InvalidTokenException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((InvalidTokenException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToString() ?? "");
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = ResourcesVI.SystemError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToString() ?? "");
                    break;
            }
        }
    }
}
