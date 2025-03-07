using R_Exam.Domain.Models;

namespace R_Exam.Domain.Repositories
{
    public interface ILogRepository
    {
        public void CreateRequestLog(RequestLog log);
        public void CreateResponseLog(ResponseLog log);
    }
}
