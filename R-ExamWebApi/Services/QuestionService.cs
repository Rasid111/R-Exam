using R_Exam.Exceptions;
using Models;
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
            if (question.Answers.Find((answer) => answer.Title == question.CorrectAnswerTitle) == null)
                throw new ArgumentException("Question must have a correct answer", nameof(question));
            this.repository.Create(question);
        }
        public Question GetQuestion(int id)
        {
            var question = this.repository.Get(id);
            // я могу тут вместо кастомного QuestionNotFoundException использовать KeyNotFoundException?
            return question ?? throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
        public void UpdateQuestion(Question question)
        {
            if (question.Answers.Find((answer) => answer.Title == question.CorrectAnswerTitle) == null)
                throw new ArgumentException("Question must have a correct answer", nameof(question));
            var resultStatus = this.repository.Update(question);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(question.Id), "Question was not found");
        }
        public void DeleteQuestion(int id)
        {
            var resultStatus = this.repository.Delete(id);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
    }
}