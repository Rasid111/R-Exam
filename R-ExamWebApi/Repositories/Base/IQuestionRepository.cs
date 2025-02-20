using Models;

namespace R_Exam.Repositories.Base
{
    public interface IQuestionRepository
    {
        public void CreateQuestion(Question question);
        public Question? GetQuestion(int id);
        public bool UpdateQuestion(Question questionData);
        public bool DeleteQuestion(int id);
    }
}
