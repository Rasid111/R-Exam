using FluentValidation;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using R_Exam.Domain.Repositories;
using R_Exam.Application.Exceptions;
using Microsoft.Data.SqlClient;

namespace R_Exam.Infrastructre.Services
{
    public class QuestionService(IQuestionRepository repository, IValidator<Question> validator) : IQuestionService
    {
        private readonly IQuestionRepository repository = repository;
        private readonly IValidator<Question> validator = validator;

        public async Task<int> Create(Question question)
        {
            var validationResult = await validator.ValidateAsync(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            try
            {
                int questionId = await repository.Create(question);
                return questionId;
            }
            catch (SqlException)
            {
                throw new ArgumentException("There is already exist question with the same title", nameof(question));
            }
        }
        public async Task<List<Question>> Get()
        {
            var questions = repository.Get();
            // я могу тут вместо кастомного QuestionNotFoundException использовать KeyNotFoundException?
            return await questions;
        }
        public async Task<Question> Get(int id)
        {
            var question = repository.Get(id);
            // я могу тут вместо кастомного QuestionNotFoundException использовать KeyNotFoundException?
            return await question ?? throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
        public async Task Update(Question question)
        {
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            var resultStatus = await repository.Update(question);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(question.Id), "Question was not found");
        }
        public async Task Delete(int id)
        {
            var resultStatus = await repository.Delete(id);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(id), "Question was not found");
        }
    }
}