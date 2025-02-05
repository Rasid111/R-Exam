using R_Exam.Models;

namespace R_Exam.Repositories.Base
{
    public interface IQuestionRepository
    {
        public void CreateQuestion(Question question);
        public Question GetQuestion(int id);
        public void UpdateQuestion(Question questionData);
        public void DeleteQuestion(int id);
    }
}
