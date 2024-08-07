using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.Dtos.TeamMember;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.TeamMember.Command
{
    public class AddUpdateTeamMemberCommand : TeamMemberDto, IRequest<Result>
    {
    }
    public class AddUpdateTeamMemberCommandHandler : IRequestHandler<AddUpdateTeamMemberCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AddUpdateTeamMemberCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AddUpdateTeamMemberCommand request, CancellationToken cancellationToken)
        {
            var teamMember = await _context.Set<LU_TeamMember>().FindAsync(request.Id);
            var existsRecord = new LU_TeamMember();
            if (request.Id > 0 && teamMember == null)
                return Result.Failure(new string[] { "Team Member not found" });

            else if (request.Id > 0 && teamMember != null)
                existsRecord = await _context.Set<LU_TeamMember>().FirstOrDefaultAsync(x => x.Id != request.Id
                 && x.Name == request.Name && x.IsDeleted != true);
            else if (request.Id == 0 && teamMember == null)
                existsRecord = await _context.Set<LU_TeamMember>().FirstOrDefaultAsync(x => x.Name == request.Name && x.IsDeleted != true);
            if (existsRecord != null)
                return Result.Failure(new string[] { "Team Member already exists with this name." });
            teamMember = teamMember == null ? new LU_TeamMember() : teamMember;
            teamMember.Name = request.Name;
            if (request.Id == 0)
                _context.Set<LU_TeamMember>().Add(teamMember);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
