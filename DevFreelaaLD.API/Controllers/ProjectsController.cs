using DevFreelaaLD.API.Models;
using DevFreelaaLD.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectsController(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET // api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            return Ok();
        }

        // GET // api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // POST // api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel projectInputModel)
        {
            return CreatedAtAction(nameof(GetById), new {id = 1}, projectInputModel);
        }

        // PUT // api/projects/123
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel updateProjectInputModel)
        {
            return NoContent();
        }

        // DELETE // api/projects/123
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }


        // PUT // api/projects/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }


        // PUT // api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            return NoContent();
        }

        // POST // api/projects/123/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel commentInputModel)
        {
            return Ok();
        }
    }
}
