using R_Exam.Domain.Models;

namespace R_Exam.Application.Services
{
    public interface IQuestionService
    {
        public void Create(Question question);
        public List<Question> Get();
        public Question Get(int id);
        public void Update(Question question);
        public void Delete(int id);
    }
}
