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
    public class AnswerController : BaseApiClontroller
    {
        public AnswerController(ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration) : base(dbContext, roleManager, userManager, configuration)
        {
        }

        [HttpGet("{id}", Name = "GetAnswer")]
        public IActionResult Get(int id)
        {
            var answer = DbContext.Answers.FirstOrDefault(i => i.Id == id);

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = $"Answer ID {id} has not been found"
                });
            }

            return Ok(answer.Adapt<AnswerViewModel>());
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] AnswerViewModel model)
        {
            if (model == null) return BadRequest();

            var answer = DbContext.Answers.FirstOrDefault(q => q.Id == model.Id);

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = $"Answer ID {model.Id} has not been found"
                });
            }

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Value = model.Value;
            answer.Notes = model.Notes;
            answer.LastModifiedDate = answer.CreatedDate;

            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AnswerViewModel model)
        {
            if (model == null) return BadRequest();

            var answer = model.Adapt<Answer>();

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;
            answer.CreatedDate = DateTime.Now;
            answer.LastModifiedDate = answer.CreatedDate;

            DbContext.Answers.Add(answer);
            DbContext.SaveChanges();

            return Created("GetAnswer", answer);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var answer = DbContext.Answers.FirstOrDefault(i => i.Id == id);

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = $"Answer ID {id} has not been"
                });
            }

            DbContext.Answers.Remove(answer);
            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var answers = DbContext.Answers.Where(q => q.QuestionId == questionId).ToArray();

            return Ok(answers.Adapt<AnswerViewModel[]>());
        }
    }
}