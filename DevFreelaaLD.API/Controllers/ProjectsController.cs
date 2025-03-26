using DevFreelaaLD.API.Models;
using DevFreelaaLD.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProjectsController : ControllerBase
    {
        private readonly FreelanceTotalCostConfig _config;
        private readonly IConfigServices _configServices;

        public ProjectsController(IOptions<FreelanceTotalCostConfig> totalCostOptions, IConfigServices configServices)
        {
            _config = totalCostOptions.Value;
            _configServices = configServices;
        }

        // GET // api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "")
        {
            return Ok(_configServices.GetValue());
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
            if (projectInputModel.TotalCost < _config.Maximum || projectInputModel.TotalCost > _config.Minimum)
                return BadRequest("number out of bounds.");

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
