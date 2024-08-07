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

namespace Anubis.Application.FreightCompany.Queries.Documents
{

    public class GridCompanyDocumentsQuery : GridQuery, IRequest<GridResult<CompanyDocuments>>
    {

    }
    public class GridFTLQueryHandler : IRequestHandler<GridCompanyDocumentsQuery, GridResult<CompanyDocuments>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridFTLQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<CompanyDocuments>> Handle(GridCompanyDocumentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<CompanyDocuments>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
    .DynamicPageAsync(request, cancellationToken);
        }
    }
}
