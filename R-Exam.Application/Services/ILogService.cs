using Microsoft.AspNetCore.Http;

namespace R_Exam.Application.Services
{
    public interface ILogService
    {
        public void CreateRequestLog(HttpContext httpContext);
        public void CreateResponseLog(HttpContext httpContext);
    }
}
