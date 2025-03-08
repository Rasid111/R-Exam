using Microsoft.AspNetCore.Mvc;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using System.Net;
using MediatR;
using R_Exam.Application.Dtos.Question;

namespace R_Exam.Presentation.Controllers
{
    [Route("/api/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class QuestionController(IQuestionService questionService, ISender sender) : ControllerBase
    {

        private readonly IQuestionService questionService = questionService;
        private readonly ISender sender = sender;
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateQuestion([FromBody] QuestionCreateRequestDto question)
        {
            var response = await this.sender.Send(question);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<QuestionGetResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> GetQuestions()
        {
            var response = await this.sender.Send(new QuestionGetAllRequestDto());
            return response.Count == 0 ? NoContent() : Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuestionGetResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetQuestion(int id)
        {
            var response = await this.sender.Send(new QuestionGetRequestDto(id: id));
            return Ok(response);
        }

        [HttpPatch()]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Update([FromBody] QuestionUpdateRequestDto question)
        {
            await this.sender.Send(question);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Remove(int id)
        {
            await questionService.Delete(id);
            return Ok();
        }
    }
}
