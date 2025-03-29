using AutoMapper;
using Moq;
using R_Exam.Application.Dtos.Question;
using R_Exam.Application.Handlers.Question;
using R_Exam.Application.Mappers;
using R_Exam.Application.Services;
using R_Exam.Application.Validators;
using R_Exam.Domain.Models;

namespace R_Exam.Tests.Handlers.Question
{
    public class QuestionGetAllHandlerTests
    {
        [Fact]
        public async Task Handle_RepositoryReturnsListOfQuestions_ListOfQuestionDtos()
        {
            List<Domain.Models.Question> questions = [
                new()
                {
                    Id = 1,
                    Title = "Test",
                    CorrectAnswerTitle = "Test",
                    Answers = [new Answer() { Title = "Test" }],
                }
            ];

            List<QuestionGetResponseDto> excpectedDto = [
                new()
                {
                    Id = 1,
                    Title = "Test",
                    CorrectAnswerTitle = "Test",
                    Answers = [ "Test" ],
                }
            ];

            var dto = new QuestionGetAllRequestDto();
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Get())
                .ReturnsAsync(questions);

            QuestionGetAllHandler handler = new QuestionGetAllHandler(new QuestionService(
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
        public async Task Handle_RepositoryReturnsEmptyList_EmptyListOf()
        {
            List<Domain.Models.Question> questions = [];

            List<QuestionGetResponseDto> excpectedDto = [];

            var dto = new QuestionGetAllRequestDto();
            var repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock.Setup(repo => repo.Get())
                .ReturnsAsync(questions);

            QuestionGetAllHandler handler = new QuestionGetAllHandler(new QuestionService(
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
    }
}