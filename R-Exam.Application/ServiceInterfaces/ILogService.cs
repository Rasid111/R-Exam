using Microsoft.AspNetCore.Http;

namespace R_Exam.Application.Services
{
    public interface ILogService
    {
        public Task CreateRequestLog(HttpContext httpContext);
        public Task CreateResponseLog(HttpContext httpContext);
    }
}
