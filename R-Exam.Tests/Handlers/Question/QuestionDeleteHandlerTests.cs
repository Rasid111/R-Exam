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
    public class QuestionDeleteHandlerTests
    {
        [Fact]
        public async Task Handle_RepositoryReturnsTrue_Void()
        {
            var dto = new QuestionDeleteRequestDto(1);
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Delete(dto.Id))
                .ReturnsAsync(true);

            var handler = new QuestionDeleteHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));
            await handler.Handle(dto, CancellationToken.None);
        }
        [Fact]
        public async Task Handle_RepositoryReturnsFalse_Void()
        {
            var dto = new QuestionDeleteRequestDto(1);
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Delete(dto.Id))
                .ReturnsAsync(false);

            var handler = new QuestionDeleteHandler(new QuestionService(
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
