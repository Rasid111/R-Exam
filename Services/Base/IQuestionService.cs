using R_Exam.Models;

namespace R_Exam.Services.Base
{
    public interface IQuestionService
    {
        public bool CreateQuestion(Question question);
        public Question GetQuestion(int id);
        public bool UpdateQuestion(Question question);
        public bool DeleteQuestion(int id);
    }
}
