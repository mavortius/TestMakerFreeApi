using System;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestMakerFreeApi.Data;
using TestMakerFreeApi.Data.Models;
using TestMakerFreeApi.ViewModels;

namespace TestMakerFreeApi.Controllers
{
    public class QuizController : BaseApiClontroller
    {
        public QuizController(ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration) : base(dbContext, roleManager, userManager, configuration)
        {
        }

        [HttpGet("{id}", Name = "GetQuiz")]
        public IActionResult Get(int id)
        {
            var quiz = DbContext.Quizzes.FirstOrDefault(i => i.Id == id);

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            return Ok(quiz.Adapt<QuizViewModel>());
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuizViewModel model)
        {
            if (model == null) return new BadRequestResult();

            var quiz = DbContext.Quizzes.FirstOrDefault(q => q.Id == model.Id);

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", model.Id)
                });
            }

            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;
            quiz.LastModifiedDate = quiz.CreatedDate;

            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuizViewModel model)
        {
            if (model == null) return new BadRequestResult();

            var quiz = new Quiz
            {
                Title = model.Title,
                Description = model.Description,
                Text = model.Text,
                Notes = model.Notes,
                CreatedDate = DateTime.Now
            };

            quiz.LastModifiedDate = quiz.CreatedDate;
            quiz.UserId = DbContext.Users.FirstOrDefault(u => u.UserName == "Admin")?.Id;

            DbContext.Quizzes.Add(quiz);
            DbContext.SaveChanges();

            return Created("GetQuiz", quiz.Adapt<QuizViewModel>());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = DbContext.Quizzes.FirstOrDefault(i => i.Id == id);

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            DbContext.Quizzes.Remove(quiz);
            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(q => q.CreatedDate)
                .Take(num)
                .ToArray();

            return Ok(latest.Adapt<QuizViewModel[]>());
        }

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var byTitle = DbContext.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ToArray();

            return Ok(byTitle.Adapt<QuizViewModel[]>());
        }

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var ramdom = DbContext.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ToArray();

            return Ok(ramdom.Adapt<QuizViewModel[]>());
        }
    }
}