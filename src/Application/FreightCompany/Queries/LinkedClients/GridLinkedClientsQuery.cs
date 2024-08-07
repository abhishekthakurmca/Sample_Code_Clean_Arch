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

namespace Anubis.Application.FreightCompany.Queries.LinkedClients
{
    public class GridLinkedClientsQuery : GridQuery, IRequest<GridResult<CompanyLinkedClients>>
    {

    }
    public class GridLinkedClientsQueryHandler : IRequestHandler<GridLinkedClientsQuery, GridResult<CompanyLinkedClients>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridLinkedClientsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<CompanyLinkedClients>> Handle(GridLinkedClientsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<CompanyLinkedClients>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                .DynamicPageAsync(request, cancellationToken);
        }
    }
}
