using Anubis.Application.Common.Interfaces;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.TeamMember.Query
{
    public class GetAllTeamMembersDDLQuery : IRequest<List<OptionVm>>
    {
    }
    public class GetAllTeamMembersDDLQueryHandler : IRequestHandler<GetAllTeamMembersDDLQuery, List<OptionVm>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllTeamMembersDDLQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OptionVm>> Handle(GetAllTeamMembersDDLQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<LU_TeamMember>().Where(x => x.IsDeleted != true)
                .Select(x => new OptionVm { Text = x.Name, Value = x.Id.ToString() }).ToListAsync();
        }
    }
}
