using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.GetCompanyContacts;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCategoryDB.Query
{
    public class GridCategoryQuery : GridQuery, IRequest<GridResult<FreightCategory>>
    {

    }
    public class GridCategoryQueryHandler : IRequestHandler<GridCategoryQuery, GridResult<FreightCategory>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<FreightCategory>> Handle(GridCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<FreightCategory>().Where(x => x.IsDeleted != true)
                .DynamicPageAsync(request, cancellationToken);
        }
    }
}
