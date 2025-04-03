using DevFreela.Application.Models;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly IMediator _mediator;

        public InsertProjectHandler(DevFreelaDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var projects = request.ToEntity();

            await _dbContext.Projects.AddAsync(projects);
            await _dbContext.SaveChangesAsync();

            var projectCreated = new ProjectCreatedNotification(projects.Id, projects.Title, projects.TotalCost);

            await _mediator.Publish(projectCreated);

            return ResultViewModel<int>.Success(projects.Id);
        }
    }
}
