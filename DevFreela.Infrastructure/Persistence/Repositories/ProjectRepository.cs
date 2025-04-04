﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();

            return project.Id;
        }

        public async Task AddComment(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return projects;
        }

        public async Task<Project?> GetById(int id)
        {
            return await _dbContext.Projects
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            var project = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task Update(Project project)
        {
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();
        }
    }
}
