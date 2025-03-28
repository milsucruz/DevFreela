using DevFreelaaLD.API.Entities;
using DevFreelaaLD.API.Models;
using DevFreelaaLD.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillsController(DevFreelaDbContext dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _dbContext.Skills.ToList();

            return Ok(skills);
        }

        [HttpPost]
        public IActionResult Post(CreateSkillInputModel skillInputModel) 
        {
            var skill = new Skill(skillInputModel.Description);

            _dbContext.Skills.Add(skill);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
