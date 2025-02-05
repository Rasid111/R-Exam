using R_Exam.Models;
using R_Exam.Repositories.Base;
using R_Exam.Services.Base;

namespace R_Exam.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository repository;

        public QuestionService(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public bool CreateQuestion(Question question)
        {
            this.repository.CreateQuestion(question);
            return true;
        }
        public Question GetQuestion(int id)
        {
            var question = this.repository.GetQuestion(id);
            return question;
        }
        public bool UpdateQuestion(Question question)
        {
            this.repository.UpdateQuestion(question);
            return true;
        }
        public bool DeleteQuestion(int id)
        {
            this.repository.DeleteQuestion(id);
            return true;
        }
    }
}