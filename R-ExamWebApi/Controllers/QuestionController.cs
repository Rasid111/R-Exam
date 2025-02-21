using Microsoft.AspNetCore.Mvc;
using R_Exam.Exceptions;
using Models;
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
            try
            {
                questionService.CreateQuestion(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Question), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetQuestion(int id)
        {
            Question question;
            try
            {
                question = questionService.GetQuestion(id);
            }
            catch (QuestionNotFoundException)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPatch()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Update([FromBody] Question question)
        {
            try
            {
                questionService.UpdateQuestion(question);
            }
            catch (QuestionNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult Remove(int id)
        {
            try
            {
                questionService.DeleteQuestion(id);
            }
            catch (QuestionNotFoundException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
