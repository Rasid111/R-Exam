using R_Exam.Domain.Models;

namespace R_Exam.Domain.Repositories
{
    public interface ILogRepository
    {
        public Task CreateRequestLog(RequestLog log);
        public Task CreateResponseLog(ResponseLog log);
    }
}
