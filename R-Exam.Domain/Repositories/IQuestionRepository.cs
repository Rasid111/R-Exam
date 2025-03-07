using R_Exam.Domain.Models;

#pragma warning disable IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
namespace R_Exam.Domain.Repositories
#pragma warning restore IDE0130 // Пространство имен (namespace) не соответствует структуре папок.
{
    public interface IQuestionRepository
    {
        public void Create(Question question);
        public List<Question> Get();
        public Question? Get(int id);
        public bool Update(Question questionData);
        public bool Delete(int id);
    }
}
