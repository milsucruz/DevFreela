﻿using DevFreelaaLD.API.Entities;
using DevFreelaaLD.API.Models;
using DevFreelaaLD.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreelaaLD.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DevFreelaDbContext _dbContext;

        public UsersController(DevFreelaDbContext dbContext)
        {
            dbContext = _dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _dbContext.Users
                .Include(u => u.Skills)
                    .ThenInclude(u => u.Skill)
                .SingleOrDefault();

            if(user is null)
                return NotFound();

            var model = UserViewModel.FromEntity(user);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(CreateUserInputModel userInputModel)
        {
            var user = new User(userInputModel.FullName, userInputModel.Email, userInputModel.BirthDate);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel userSkillsInputModel)
        {
            var userSkills = userSkillsInputModel.SkillsIds.Select(s => new UserSkill(id, s)).ToList();

            _dbContext.UserSkills.AddRange(userSkills);
            _dbContext.SaveChanges();

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