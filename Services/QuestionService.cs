using R_Exam.Exceptions;
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

        public void CreateQuestion(Question question)
        {
            this.repository.CreateQuestion(question);
        }
        public Question GetQuestion(int id)
        {
            var question = this.repository.GetQuestion(id);

            return question ?? throw new QuestionNotFoundException();
        }
        public void UpdateQuestion(Question question)
        {
            var resultStatus = this.repository.UpdateQuestion(question);
            if (!resultStatus)
                throw new QuestionNotFoundException();
        }
        public void DeleteQuestion(int id)
        {
            var resultStatus = this.repository.DeleteQuestion(id);
            if (!resultStatus)
                throw new QuestionNotFoundException();
        }
    }
}