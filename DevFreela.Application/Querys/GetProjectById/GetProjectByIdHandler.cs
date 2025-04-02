using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Querys.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetProjectByIdHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.FreeLancer)
                .Include(p => p.Comments)
                .SingleOrDefaultAsync(p => p.Id == request.Id);

            if (projects is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntity(projects);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
