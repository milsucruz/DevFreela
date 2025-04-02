using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }

}
