using Microsoft.AspNetCore.Mvc;
using R_Exam.Models;
using R_Exam.Repositories.Base;
using R_Exam.Services.Base;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace R_Exam.Controllers
{
    [Route("/[controller]")]
    public class QuestionController : ControllerBase
    {

        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult CreateQuestion([FromBody] Question question)
        {
            var resultStatus = questionService.CreateQuestion(question);
            return resultStatus ? base.Ok() : base.BadRequest();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetQuestion(int id)
        {
            var question = questionService.GetQuestion(id);
            return question != null ? base.Ok(question) : base.NotFound();
        }

        [HttpPatch("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult UpdateQuestion([FromBody] Question question)
        {
            var resultStatus = questionService.UpdateQuestion(question);
            return resultStatus ? base.Ok() : base.BadRequest();
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult RemoveQuestion(int id)
        {
            var resultStatus = questionService.DeleteQuestion(id);
            return resultStatus ? base.Ok() : base.NotFound();
        }
    }
}
