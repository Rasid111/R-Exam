using R_Exam.Domain.Models;

#pragma warning disable IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
namespace R_Exam.Domain.Repositories
#pragma warning restore IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
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
