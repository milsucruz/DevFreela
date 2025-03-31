using DevFreela.Application.Models;
using DevFreela.Application.Services;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IProjectServices _services;

        public ProjectsController(DevFreelaDbContext dbContext, IProjectServices services)
        {
            _dbContext = dbContext;
            _services = services;
        }

        // GET // api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var result = _services.GetAll();

            return Ok(result);
        }

        // GET // api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _services.GetById(id);

            if(!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST // api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel projectInputModel)
        {
            var result = _services.Insert(projectInputModel);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, projectInputModel);
        }

        // PUT // api/projects/123
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel updateProjectInputModel)
        {
            var result = _services.Update(updateProjectInputModel);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // DELETE // api/projects/123
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _services.Delete(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT // api/projects/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var result = _services.Start(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT // api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var result = _services.Complete(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // POST // api/projects/123/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel commentInputModel)
        {
            var result = _services.Complete(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}