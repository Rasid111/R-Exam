using R_Exam.WebApi.Core.Models;

namespace R_Exam.WebApi.Core.Repositories.Base
{
    public interface IQuestionRepository
    {
        public void Create(Question question);
        public Question? Get(int id);
        public bool Update(Question questionData);
        public bool Delete(int id);
    }
}
