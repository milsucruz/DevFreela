using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Querys.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly IProjectRepository _repository;

        public GetProjectByIdHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetDetailsById(request.Id);

            if (projects is null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntity(projects);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
