using R_Exam.Domain.Models;

namespace R_Exam.Domain.Models
{
    public interface IQuestionRepository
    {
        public Task<int> Create(Question question);
        public Task<List<Question>> Get();
        public Task<Question?> Get(int id);
        public Task<bool> Update(Question questionData);
        public Task<bool> Delete(int id);
    }
}
