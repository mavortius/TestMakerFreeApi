using System;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestMakerFreeApi.Data;
using TestMakerFreeApi.Data.Models;
using TestMakerFreeApi.ViewModels;

namespace TestMakerFreeApi.Controllers
{
    public class QuestionController : BaseApiClontroller
    {
        public QuestionController(ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration) : base(dbContext, roleManager, userManager, configuration)
        {
        }

        [HttpGet("{id}", Name = "GetQuestion")]
        public IActionResult Get(int id)
        {
            var question = DbContext.Questions.FirstOrDefault(i => i.Id == id);

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            return Ok(question.Adapt<QuestionViewModel>());
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] QuestionViewModel model)
        {
            if (model == null) return BadRequest();

            var question = DbContext.Questions.FirstOrDefault(q => q.Id == model.Id);

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", model.Id)
                });
            }

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.LastModifiedDate = question.CreatedDate;

            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] QuestionViewModel model)
        {
            if (model == null) return BadRequest();

            var question = model.Adapt<Question>();

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.CreatedDate = DateTime.Now;
            question.LastModifiedDate = question.CreatedDate;

            DbContext.Questions.Add(question);
            DbContext.SaveChanges();

            return Created("GetQuestion", question.Adapt<QuestionViewModel>());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var question = DbContext.Questions.FirstOrDefault(i => i.Id == id);

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            DbContext.Questions.Remove(question);
            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var questions = DbContext.Questions.Where(q => q.QuizId == quizId).ToArray();

            return Ok(questions.Adapt<QuestionViewModel[]>());
        }
    }
}