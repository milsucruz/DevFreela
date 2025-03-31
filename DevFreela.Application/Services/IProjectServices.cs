using Azure;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DevFreela.Application.Services
{
    public interface IProjectServices
    {
        ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3);

        ResultViewModel<ProjectViewModel> GetById(int id);

        ResultViewModel<int> Insert(CreateProjectInputModel createProjectInputModel);

        ResultViewModel Update(UpdateProjectInputModel updateProjectInputModel);

        ResultViewModel Delete(int id);

        ResultViewModel Start(int id);

        ResultViewModel Complete(int id);

        ResultViewModel InsertCommente(int id, CreateProjectCommentInputModel createProjectCommentInputModel);
    }

    public class ProjectService : IProjectServices
    {
        private readonly DevFreelaDbContext _dbContext;

        public ProjectService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultViewModel Complete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            project.Complete();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Delete(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            project.SetAsDeleted();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int size = 3)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * size)
                .Take(size)
                .ToList();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var projects = _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);

            if (projects is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntity(projects);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel projectInputModel)
        {
            var projects = projectInputModel.ToEntity();

            _dbContext.Projects.Add(projects);
            _dbContext.SaveChanges();

            return ResultViewModel<int>.Success(projects.Id);
        }

        public ResultViewModel InsertCommente(int id, CreateProjectCommentInputModel projectCommentInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            var comment = new ProjectComment(projectCommentInputModel.Content, projectCommentInputModel.IdProject, projectCommentInputModel.IdUser);

            _dbContext.ProjectComments.Add(comment);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            project.Start();

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }

        public ResultViewModel Update(UpdateProjectInputModel updateProjectInputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == updateProjectInputModel.IdProject);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            project.Update(updateProjectInputModel.Title, updateProjectInputModel.Description, updateProjectInputModel.TotalCost);

            _dbContext.Update(project);
            _dbContext.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}