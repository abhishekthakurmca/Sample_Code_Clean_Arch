using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.TeamMember.Command
{
    public class RemoveTeamMemberCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class RemoveTeamMemberCommandHandler : IRequestHandler<RemoveTeamMemberCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RemoveTeamMemberCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
        {
            var teamMember = await _context.Set<LU_TeamMember>().FindAsync(request.Id);
            if (request.Id > 0 && teamMember == null)
                return Result.Failure(new string[] { "Record not found" });
            teamMember.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
