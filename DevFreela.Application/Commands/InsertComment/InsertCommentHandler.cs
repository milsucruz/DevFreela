using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public InsertCommentHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
                return ResultViewModel.Error("Projeto não encontrado");

            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);

            await _dbContext.ProjectComments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
