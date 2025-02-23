using Models;
using R_Exam.Services.Base;

namespace R_Exam.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;
        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogService logService)
        {
            logService.CreateRequestLog(httpContext);
            await this.next.Invoke(httpContext);
            logService.CreateResponseLog(httpContext);
        }
    }
}
