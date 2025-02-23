using Models;

namespace R_Exam.Services.Base
{
    public interface IQuestionService
    {
        public void CreateQuestion(Question question);
        public Question GetQuestion(int id);
        public void UpdateQuestion(Question question);
        public void DeleteQuestion(int id);
    }
}
