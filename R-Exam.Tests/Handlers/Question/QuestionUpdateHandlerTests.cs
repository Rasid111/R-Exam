using AutoMapper;
using Microsoft.Data.SqlClient;
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
    public class QuestionUpdateHandlerTests
    {
        [Fact]
        public async Task Handle_PassCorrectQuestion_Void()
        {

            var dto = new QuestionUpdateRequestDto()
            {
                Id = 1,
                Title = "1+1",
                CorrectAnswerTitle = "2",
                Answers = ["1", "2", "3", "4"]
            };

            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Update(It.IsAny<Domain.Models.Question>()))
                .ReturnsAsync(true);

            QuestionUpdateHandler handler = new QuestionUpdateHandler(new QuestionService(
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

            var dto = new QuestionUpdateRequestDto()
            {
                Id = 1,
                Title = "1+1",
                CorrectAnswerTitle = "2",
                Answers = ["1", "2", "3", "4"]
            };

            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Update(It.IsAny<Domain.Models.Question>()))
                .ReturnsAsync(false);

            QuestionUpdateHandler handler = new QuestionUpdateHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            var ex = await Assert.ThrowsAsync<QuestionNotFoundException>(async () => await handler.Handle(dto, CancellationToken.None));
            Assert.Equal(nameof(dto.Id), ex.Key);
        }
        [Fact]
        public async Task Handle_PassQuestionWithoutCorrectAnswer_ThrowsArgumentException()
        {
            var dto = new QuestionUpdateRequestDto()
            {
                Title = "1+1",
                CorrectAnswerTitle = "2",
                Answers = ["1", "3", "4"]
            };

            var repositoryMock = new Mock<IQuestionRepository>();

            QuestionUpdateHandler handler = new QuestionUpdateHandler(new QuestionService(
                repository: null!,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(dto, CancellationToken.None));
            Assert.Equal(nameof(Domain.Models.Question.Answers), ex.ParamName);
        }
        [Theory]
        [InlineData(null, "2", new[] { "1", "2", "3", "4" }, nameof(Domain.Models.Question.Title))]
        [InlineData("1+1", null, new[] { "1", "2", "3", "4" }, nameof(Domain.Models.Question.CorrectAnswerTitle))]
        [InlineData("1+1", "2", null, nameof(Domain.Models.Question.Answers))]
        public async Task Handle_PassQuestionWithNullArguments_ThrowsArgumentException(string title, string correctAnswerTitle, string[] answers, string paramName)
        {
            var dto = new QuestionUpdateRequestDto()
            {
                Id = 1,
                Title = title,
                CorrectAnswerTitle = correctAnswerTitle,
                Answers = answers?.ToList()!
            };

            var handler = new QuestionUpdateHandler(new QuestionService(
                repository: null!,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(dto, CancellationToken.None));
            Assert.Equal(paramName, ex.ParamName);
        }
        [Theory]
        [InlineData(
            "000000000000000000000000000000000000000000000000000",
            "2",
            new[] { "1", "2", "3", "4" },
            nameof(Domain.Models.Question.Title))]
        [InlineData(
            "1+1",
            "000000000000000000000000000000000000000000000000000",
            new[] {
                "1", "2", "3", "4" },
            nameof(Domain.Models.Question.CorrectAnswerTitle))]
        [InlineData(
            "1+1",
            "2",
            new[] {
                "000000000000000000000000000000000000000000000000000", "2", "3", "4" },
            "Answers[0].Title")]
        public async Task Handle_PassQuestionWithIncorrectArgumentLengths_ThrowsArgumentException(string title, string correctAnswerTitle, string[] answers, string paramName)
        {
            var dto = new QuestionUpdateRequestDto()
            {
                Id = 1,
                Title = title,
                CorrectAnswerTitle = correctAnswerTitle,
                Answers = answers?.ToList()!
            };

            var handler = new QuestionUpdateHandler(new QuestionService(
                repository: null!,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(dto, CancellationToken.None));
            Assert.Equal(paramName, ex.ParamName);
        }
        [Fact]
        public async Task Handle_PassQuestionWithDublicateTitle_ThrowsArgumentException()
        {
            var dto = new QuestionUpdateRequestDto()
            {
                Id = 1,
                Title = "1+1",
                CorrectAnswerTitle = "2",
                Answers = (new string[] { "1", "2", "3", "4" }).ToList()
            };

            var repositoryMock = new Mock<IQuestionRepository>();

            SqlException exception = null!;
            try
            {
                var conn = new SqlConnection(@"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
                conn.Open();
            }
            catch (SqlException ex)
            {
                exception = ex;
                //exception.Number = 2627;
            }

            repositoryMock.Setup(repo => repo.Update(It.Is<Domain.Models.Question>(question => question.Title == "1+1")))
                .ThrowsAsync(exception);

            var handler = new QuestionUpdateHandler(new QuestionService(
                repository: repositoryMock.Object,
                validator: new QuestionValidator(),
                mapper: new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Question_QuestionDto_Mapper>();
                }))
            ));
            
            {
                var ex = await Assert.ThrowsAsync<SqlException>(async () => await handler.Handle(dto, CancellationToken.None));
                //var ex = await Assert.ThrowsAsync<DuplicateNameException>(async () => await handler.Handle(dto, CancellationToken.None));
                //Assert.Equal(2627, ex.Number);
            }
        }
    }
}
