
using AutoMapper;
using Moq;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Exceptions;
using R_Exam.Application.Handlers.Question;
using R_Exam.Application.Mappers;
using R_Exam.Application.Services;
using R_Exam.Application.Validators;
using R_Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Tests.Handlers.Question
{
    public class QuestionGetHandlerTests
    {
        [Fact]
        public async Task Handle_RepositoryReturnsQuestionInstance_QuestionDto()
        {
            Domain.Models.Question question = new()
            {
                Id = 1,
                Title = "Test",
                CorrectAnswerTitle = "Test",
                Answers = [new Answer() { Title = "Test" }],
            };

            QuestionGetResponseDto excpectedDto = new()
            {
                Id = 1,
                Title = "Test",
                CorrectAnswerTitle = "Test",
                Answers = ["Test"],
            };

            var dto = new QuestionGetRequestDto(1);
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Get(dto.Id))
                .ReturnsAsync(question);

            var handler = new QuestionGetHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            var result = await handler.Handle(dto, CancellationToken.None);

            Assert.Equivalent(excpectedDto, result);
        }
        [Fact]
        public async Task Handle_RepositoryReturnsNull_ThrowsQuestionNotFoundException()
        {
            var dto = new QuestionGetRequestDto(1);
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Get(dto.Id))
                .ReturnsAsync(value: null);

            var handler = new QuestionGetHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            await Assert.ThrowsAsync<QuestionNotFoundException>(async () => await handler.Handle(dto, CancellationToken.None));
        }
    }
}
