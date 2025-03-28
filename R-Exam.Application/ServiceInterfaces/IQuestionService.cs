using R_Exam.Application.Dtos.Question;
using R_Exam.Domain.Models;

namespace R_Exam.Application.Services
{
    public interface IQuestionService
    {
        public Task<QuestionCreateResponseDto> Create(QuestionCreateRequestDto dto);
        public Task<List<QuestionGetResponseDto>> Get();
        public Task<QuestionGetResponseDto> Get(QuestionGetRequestDto dto);
        public Task Update(QuestionUpdateRequestDto dto);
        public Task Delete(QuestionDeleteRequestDto dto);
    }
}
