using DevFreela.Application.Commands.CompleteProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.InsertComment;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Querys.GetAllProjects;
using DevFreela.Application.Querys.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET // api/projects?search=crm
        [HttpGet]
        public async Task<IActionResult> Get(string search = "")
        {
            var query = new GetAllProjectsQuery();

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // GET // api/projects/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _mediator.Send(new GetProjectByIdQuery(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST // api/projects
        [HttpPost]
        public async Task<IActionResult> Post(InsertProjectCommand insertProjectCommand)
        {
            var result = await _mediator.Send(insertProjectCommand);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, insertProjectCommand);
        }

        // PUT // api/projects/123
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProjectCommand updateProjectCommand)
        {
            var result = await _mediator.Send(updateProjectCommand);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // DELETE // api/projects/123
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProjectCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT // api/projects/123/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            var result = await _mediator.Send(new StartProjectCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // PUT // api/projects/123/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            var result = await _mediator.Send(new CompleteProjectCommand(id));

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }

        // POST // api/projects/123/comments
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, InsertCommentCommand insertCommentCommand)
        {
            var result = await _mediator.Send(insertCommentCommand);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return NoContent();
        }
    }
}