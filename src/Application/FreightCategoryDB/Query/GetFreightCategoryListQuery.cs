using Anubis.Application.Common.Interfaces;
using Anubis.Application.ExportFiles.FreightProfiles.Category;
using Anubis.Application.FreightCompany.Queries;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCategoryDB.Query
{

    public class GetFreightCategoryListQuery : IRequest<List<FreightCategoryDto>>
    {
    }
    public class GetFreightCategoryListQueryHandler : IRequestHandler<GetFreightCategoryListQuery, List<FreightCategoryDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetFreightCategoryListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FreightCategoryDto>> Handle(GetFreightCategoryListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<FreightCategory>().Where(x => x.IsDeleted != true).Select(x => new FreightCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync(cancellationToken);
        }
    }
}
