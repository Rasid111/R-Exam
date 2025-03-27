using R_Exam.Domain.Models;

namespace R_Exam.Application.Services
{
    public interface IQuestionService
    {
        public Task<int> Create(Question question);
        public Task<List<Question>> Get();
        public Task<Question> Get(int id);
        public Task Update(Question question);
        public Task Delete(int id);
    }
}
