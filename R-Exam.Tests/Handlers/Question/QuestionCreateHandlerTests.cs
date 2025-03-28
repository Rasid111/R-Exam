using AutoMapper;
using Moq;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Handlers.Question;
using R_Exam.Application.Mappers;
using R_Exam.Application.Services;
using R_Exam.Application.Validators;
using R_Exam.Domain.Models;
using R_Exam.Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R_Exam.Tests.Handlers.Question
{
    public class QuestionCreateHandlerTests
    {
        [Theory]
        [InlineData("1+1", "2", new[] { "1", "2", "3", "4" })]
        public async Task Handle_PassCorrectData_CreatedQuestionId(string title, string correctAnswerTitle, string[] answers)
        {
            var dto = new QuestionCreateRequestDto()
            {
                Title = title,
                CorrectAnswerTitle = correctAnswerTitle,
                Answers = answers.ToList()
            };

            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Create(It.IsAny<Domain.Models.Question>()))
                .ReturnsAsync(1);

            var handler = new QuestionCreateHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<Question_QuestionDto_Mapper>();
                    })
                )
            ));

            var result = await handler.Handle(dto, CancellationToken.None);

            Assert.Equal(1, result.Id);
        }
    }
}
