using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Querys.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {
    }
}
