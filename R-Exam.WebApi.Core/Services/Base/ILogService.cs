using Microsoft.AspNetCore.Http;

namespace R_Exam.WebApi.Core.Services.Base
{
    public interface ILogService
    {
        public void CreateRequestLog(HttpContext httpContext);
        public void CreateResponseLog(HttpContext httpContext);
    }
}
