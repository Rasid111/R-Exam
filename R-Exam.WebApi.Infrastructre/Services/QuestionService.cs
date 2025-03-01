using FluentValidation;
using R_Exam.WebApi.Core.Models;
using R_Exam.WebApi.Core.Repositories.Base;
using R_Exam.WebApi.Core.Services.Base;
using R_Exam.WebApi.Infrastructre.Exceptions;
using R_Exam.WebApi.Infrastructre.Validators;

namespace R_Exam.WebApi.Infrastructre.Services
{
    public class QuestionService(IQuestionRepository repository, IValidator<Question> validator) : IQuestionService
    {
        private readonly IQuestionRepository repository = repository;
        private readonly IValidator<Question> validator = validator;

        public void CreateQuestion(Question question)
        {
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            repository.Create(question);
        }
        public Question GetQuestion(int id)
        {
            var question = repository.Get(id);
            // я могу тут вместо кастомного QuestionNotFoundException использовать KeyNotFoundException?
            return question ?? throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
        public void UpdateQuestion(Question question)
        {
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            var resultStatus = repository.Update(question);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(question.Id), "Question was not found");
        }
        public void DeleteQuestion(int id)
        {
            var resultStatus = repository.Delete(id);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
    }
}