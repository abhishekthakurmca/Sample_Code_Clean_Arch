using Anubis.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    public class GetLabourCostQuery : IRequest<decimal>
    {
        public long CompanyId { get; set; }
    }
    public class GetLabourCostQueryHandler : IRequestHandler<GetLabourCostQuery, decimal>
    {
        private readonly IApplicationDbContext _context;

        public GetLabourCostQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<decimal> Handle(GetLabourCostQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Set<Domain.Entities.FreightCompany>().FindAsync(request.CompanyId);
            if (company == null)
                return 0;
            return company.LabourCost == null ? 0 : company.LabourCost;
        }
    }
}
