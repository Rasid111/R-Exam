using R_Exam.WebApi.Core.Models;

namespace R_Exam.WebApi.Core.Repositories.Base
{
    public interface ILogRepository
    {
        public void CreateRequestLog(RequestLog log);
        public void CreateResponseLog(ResponseLog log);
    }
}
