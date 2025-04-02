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

        ResultViewModel InsertComment(int id, CreateProjectCommentInputModel createProjectCommentInputModel);
    }
}