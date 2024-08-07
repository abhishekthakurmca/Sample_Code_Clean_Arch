using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.GetFreightCompanies
{
    public class GridCompanyQuery : GridQuery, IRequest<GridResult<FreightCompanyDto>>
    {
        public bool isActive { get; set; }
    }
    public class GridCompanyQueryHandler : IRequestHandler<GridCompanyQuery, GridResult<FreightCompanyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridCompanyQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<FreightCompanyDto>> Handle(GridCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.Entities.FreightCompany>().Where(x => request.isActive ? x.IsActive == request.isActive && x.Id > 0 : x.Id > 0)
                 .ProjectTo<FreightCompanyDto>(_mapper.ConfigurationProvider)
                 .DynamicPageAsync(request, cancellationToken);
        }
    }
}
