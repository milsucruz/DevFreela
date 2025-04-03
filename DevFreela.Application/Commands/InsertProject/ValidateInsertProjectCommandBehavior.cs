using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.InsertProject
{
    public class ValidateInsertProjectCommandBehavior : IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _dbContext;

        public ValidateInsertProjectCommandBehavior(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, RequestHandlerDelegate<ResultViewModel<int>> next, CancellationToken cancellationToken)
        {
            var clientExists = _dbContext.Users.Any(x => x.Id == request.IdClient);

            var freelancerExists = _dbContext.Users.Any(x => x.Id == request.IdFreeLancer);

            if(!clientExists || !freelancerExists)
            {
                return ResultViewModel<int>.Error("Cliente ou Freelancer invalidos");
            }

            return await next();
        }
    }
}
