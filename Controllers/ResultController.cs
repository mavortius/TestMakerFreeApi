using System;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeApi.Data;
using TestMakerFreeApi.Data.Models;
using TestMakerFreeApi.ViewModels;

namespace TestMakerFreeApi.Controllers
{
    public class ResultController : BaseApiClontroller
    {
        public ResultController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] ResultViewModel model)
        {
            if (model == null) return BadRequest();

            var result = DbContext.Results.FirstOrDefault(q => q.Id == model.Id);
            
            if (result == null)
            {
                return NotFound(new
                {
                    Error = $"Result ID {model.Id} has not been found"
                });
            }
            
            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.Notes = model.Notes;
            result.LastModifiedDate = result.CreatedDate;

            DbContext.SaveChanges();

            return NoContent();
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null) return BadRequest();
            
            var result = model.Adapt<Result>();
            
            result.CreatedDate = DateTime.Now;
            result.LastModifiedDate = result.CreatedDate;

            DbContext.Results.Add(result);
            DbContext.SaveChanges();

            return Created("GetResult", result);
        }

        [HttpGet("{id}", Name = "GetResult")]
        public IActionResult Get(int id)
        {
            var result = DbContext.Results.FirstOrDefault(i => i.Id == id);

            if (result == null)
            {
                return NotFound(new
                {
                    Error = $"Result ID {id} has not been found"
                });
            }

            return Ok(result.Adapt<ResultViewModel>());
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = DbContext.Results.FirstOrDefault(i => i.Id == id);
            
            if (result == null)
            {
                return NotFound(new
                {
                    Error = $"Result ID {id} has not been found"
                });
            }

            DbContext.Results.Remove(result);
            DbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var results = DbContext.Results.Where(q => q.QuizId == quizId).ToArray();

            return Ok(results.Adapt<ResultViewModel[]>());
        }
    }
}