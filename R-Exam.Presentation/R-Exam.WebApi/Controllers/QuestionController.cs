using Microsoft.AspNetCore.Mvc;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using System.Net;

namespace R_Exam.Presentation.Controllers
{
    [Route("/api/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {

        private readonly IQuestionService questionService = questionService;

        [HttpPost]
        [ProducesResponseType(400)]
        public ActionResult CreateQuestion([FromBody] Question question)
        {
            questionService.Create(question);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Question>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public ActionResult GetQuestions()
        {
            List<Question> questions;
            questions = questionService.Get();
            return questions.Count == 0 ? NoContent() : Ok(questions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult GetQuestion(int id)
        {
            Question question;
            question = questionService.Get(id);
            return Ok(question);
        }

        [HttpPatch()]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult Update([FromBody] Question question)
        {
            questionService.Update(question);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult Remove(int id)
        {
            questionService.Delete(id);
            return Ok();
        }
    }
}
