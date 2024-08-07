using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Commands;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.GetFreightCompanies
{
    public class GridFreightCodeQuery : GridQuery, IRequest<GridResult<FreightCompanyCodes>>
    {

    }
    public class GridFreightCodeQueryHandler : IRequestHandler<GridFreightCodeQuery, GridResult<FreightCompanyCodes>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridFreightCodeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<FreightCompanyCodes>> Handle(GridFreightCodeQuery request, CancellationToken cancellationToken)
        {

            return await _context.Set<FreightCompanyCodes>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                // .ProjectTo<FTL_Company>(_mapper.ConfigurationProvider)
                .DynamicPageAsync(request, cancellationToken);
        }
    }
}
