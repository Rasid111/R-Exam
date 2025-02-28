using R_Exam.Exceptions;
using Models;
using R_Exam.Repositories.Base;
using R_Exam.Services.Base;
using R_Exam.Validators;

namespace R_Exam.Services
{
    public class QuestionService(IQuestionRepository repository, QuestionValidator validator) : IQuestionService
    {
        private readonly IQuestionRepository repository = repository;
        private readonly QuestionValidator validator = validator;

        public void CreateQuestion(Question question)
        {
            var validationResult = this.validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
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
            var validationResult = this.validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
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