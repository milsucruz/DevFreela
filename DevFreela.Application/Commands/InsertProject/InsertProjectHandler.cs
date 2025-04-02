using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public InsertProjectHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var projects = request.ToEntity();

            await _dbContext.Projects.AddAsync(projects);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel<int>.Success(projects.Id);
        }
    }
}
