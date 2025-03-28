using DevFreelaaLD.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(CreateUserInputModel userInputModel)
        {
            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(UserSkillsInputModel userSkillsInputModel)
        {
            return NoContent();
        }

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file)
        {
            string description = $"File: {file.FileName}, Size: {file.Length}";

            //Img

            return Ok(description);
        }
    }
}