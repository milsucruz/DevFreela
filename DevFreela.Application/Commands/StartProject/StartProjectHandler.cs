using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public StartProjectHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            project.Start();
            _dbContext.Update(project);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
