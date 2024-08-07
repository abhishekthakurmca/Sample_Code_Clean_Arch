using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.Dtos.TeamMember;
using Anubis.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.TeamMember.Query
{
    public class GetAllTeamMemberGridQuery : GridQuery, IRequest<GridResult<TeamMemberDto>>
    {
    }
    public class GetAllTeamMemberGridQueryHandler : IRequestHandler<GetAllTeamMemberGridQuery, GridResult<TeamMemberDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllTeamMemberGridQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<TeamMemberDto>> Handle(GetAllTeamMemberGridQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<LU_TeamMember>().Where(x => x.IsDeleted != true).ProjectTo<TeamMemberDto>(_mapper.ConfigurationProvider).DynamicPageAsync(request, cancellationToken);
        }
    }
}
