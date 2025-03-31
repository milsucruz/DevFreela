using DevFreelaaLD.Application.Models;
using DevFreelaaLD.Core.Entities;
using DevFreelaaLD.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Get(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return Ok(model);
        }

        // GET // api/projects/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntity(projects);

            return Ok(model);
        }

        // POST // api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel projectInputModel)
        {
            var projects = projectInputModel.ToEntity();

            _dbContext.Projects.Add(projects);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, projectInputModel);
        }

        // PUT // api/projects/123
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel updateProjectInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Update(updateProjectInputModel.Title, updateProjectInputModel.Description, updateProjectInputModel.TotalCost);

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE // api/projects/123
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.SetAsDeleted();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // PUT // api/projects/123/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Start();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // PUT // api/projects/123/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            project.Complete();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // POST // api/projects/123/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel commentInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return NotFound();

            var comment = new ProjectComment(commentInputModel.Content, commentInputModel.IdProject, commentInputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}