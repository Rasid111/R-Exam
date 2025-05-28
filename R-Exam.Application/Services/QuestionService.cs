using FluentValidation;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using R_Exam.Domain.Repositories;
using R_Exam.Application.Exceptions;
using Microsoft.Data.SqlClient;
using R_Exam.Application.Dtos.Question;
using AutoMapper;
using Azure.Core;
using System.Data;
using System.Linq;
using MediatR;
namespace R_Exam.Application.Services
{
    public class QuestionService(IQuestionRepository repository, IValidator<Question> validator, IMapper mapper) : IQuestionService
    {
        private readonly IQuestionRepository repository = repository;
        private readonly IValidator<Question> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<QuestionCreateResponseDto> Create(QuestionCreateRequestDto questionDto)
        {
            var question = mapper.Map<Question>(questionDto);
            var validationResult = await validator.ValidateAsync(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            try
            {
                int questionId = await repository.Create(question);
                return new QuestionCreateResponseDto(questionId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new DuplicateNameException($"Question with title \"{question.Title}\" already exists");
                }
                else throw;
            }
        }
        public async Task<List<QuestionGetResponseDto>> Get()
        {
            var questions = await repository.Get();
            var questionsDto = questions.Select(question => mapper.Map<QuestionGetResponseDto>(question)).ToList();
            return questionsDto;
        }
        public async Task<QuestionGetResponseDto> Get(QuestionGetRequestDto dto)
        {
            var question = await repository.Get(dto.Id);
            var questionDto = mapper.Map<QuestionGetResponseDto>(question);
            return questionDto ?? throw new QuestionNotFoundException(nameof(dto.Id), "Question was not found");
        }
        public async Task Update(QuestionUpdateRequestDto dto)
        {
            var question = mapper.Map<Question>(dto);
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage, validationResult.Errors.First().PropertyName);
            try
            {
                var resultStatus = await repository.Update(question);
                if (!resultStatus)
                    throw new QuestionNotFoundException(nameof(dto.Id), "Question was not found");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new DuplicateNameException($"Question with title \"{question.Title}\" already exists");
                }
                else throw;
            }
        }
        public async Task Delete(QuestionDeleteRequestDto dto)
        {
            var resultStatus = await repository.Delete(dto.Id);
            if (!resultStatus)
                throw new QuestionNotFoundException(nameof(dto.Id), "Question was not found");
        }
    }
}