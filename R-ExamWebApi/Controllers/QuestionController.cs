using Microsoft.AspNetCore.Mvc;
using R_Exam.Exceptions;
using Models;
using R_Exam.Repositories.Base;
using R_Exam.Services.Base;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace R_Exam.Controllers
{
    [Route("/api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public class QuestionController : ControllerBase
    {

        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public ActionResult CreateQuestion([FromBody] Question question)
        {
            questionService.CreateQuestion(question);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetQuestion(int id)
        {
            Question question;
            question = questionService.GetQuestion(id);
            return Ok(question);
        }

        [HttpPatch()]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Update([FromBody] Question question)
        {
            questionService.UpdateQuestion(question);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        public ActionResult Remove(int id)
        {
            questionService.DeleteQuestion(id);
            return Ok();
        }
    }
}
