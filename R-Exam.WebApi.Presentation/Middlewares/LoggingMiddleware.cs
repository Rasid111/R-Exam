using R_Exam.WebApi.Core.Services.Base;

namespace R_Exam.WebApi.Presentation.Middlewares
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
            await next.Invoke(httpContext);
            logService.CreateResponseLog(httpContext);
        }
    }
}
