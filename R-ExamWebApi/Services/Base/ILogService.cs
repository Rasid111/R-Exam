using Models;

namespace R_Exam.Services.Base
{
    public interface ILogService
    {
        public void CreateRequestLog(HttpContext httpContext);
        public void CreateResponseLog(HttpContext httpContext);
    }
}
