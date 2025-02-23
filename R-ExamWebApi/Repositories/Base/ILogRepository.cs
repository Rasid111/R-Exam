using Models;

namespace R_Exam.Repositories.Base
{
    public interface ILogRepository
    {
        public void CreateRequestLog(RequestLog log);
        public void CreateResponseLog(ResponseLog log);
    }
}
