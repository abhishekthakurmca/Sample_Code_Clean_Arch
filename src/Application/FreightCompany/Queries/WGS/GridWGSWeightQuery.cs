using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    public class GridWGSWeightQuery : GridQuery, IRequest<GridResult<WGSCompanyWeights>>
    {

    }
    public class GridWGSWeightQueryHandler : IRequestHandler<GridWGSWeightQuery, GridResult<WGSCompanyWeights>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridWGSWeightQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<WGSCompanyWeights>> Handle(GridWGSWeightQuery request, CancellationToken cancellationToken)
        {

            return await _context.Set<WGSCompanyWeights>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                //.ProjectTo<WGSCompanyMiles>(_mapper.ConfigurationProvider)
                .DynamicPageAsync(request, cancellationToken);
        }
    }
}
