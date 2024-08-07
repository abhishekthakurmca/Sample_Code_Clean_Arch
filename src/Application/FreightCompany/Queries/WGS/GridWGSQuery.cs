using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    public class GridWGSQuery : GridQuery, IRequest<GridWGSResult<WGSCompanyMiles, WGSCompanyWeights, WGSCompanyPrice>>
    {

    }
    public class GridWGSQueryHandler : IRequestHandler<GridWGSQuery, GridWGSResult<WGSCompanyMiles, WGSCompanyWeights, WGSCompanyPrice>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridWGSQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridWGSResult<WGSCompanyMiles, WGSCompanyWeights, WGSCompanyPrice>> Handle(GridWGSQuery request, CancellationToken cancellationToken)
        {
            var company = await _context.Set<Domain.Entities.FreightCompany>()
                 //.Include(fc => fc.ShipmentFreightTypes)
                 .Include(fc => fc.WGSCompanyMiles)
                 .Include(fc => fc.WGSCompanyWeights)
                 .ThenInclude(weight => weight.WGSCompanyPrice)
                 .FirstOrDefaultAsync(fc => fc.Id == request.Id, cancellationToken);

            var priceList = await _context.Set<WGSCompanyPrice>().Where(x => x.Company_Id == request.Id).ToListAsync();
            return new GridWGSResult<WGSCompanyMiles, WGSCompanyWeights, WGSCompanyPrice>
            {
                Miles = company.WGSCompanyMiles.ToList(),
                Weights = company.WGSCompanyWeights.ToList(),
                WeightPrice = priceList
            };
        }
    }
}
