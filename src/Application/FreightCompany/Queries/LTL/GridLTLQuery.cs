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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.LTL
{
    public class GridLTLQuery : GridQuery, IRequest<GridResult<LtlDto>>
    {

    }
    public class GridFTLQueryHandler : IRequestHandler<GridLTLQuery, GridResult<LtlDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridFTLQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<LtlDto>> Handle(GridLTLQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Set<LTL_Company>()
                        .Where(x => x.Company_Id == request.Id && !x.IsDeleted)
                        .Include(x => x.Truck)
                        .Select(x => new LtlDto
                        {
                            Id = x.Id,
                            OriginCity = x.OriginCity,
                            OriginState = x.OriginState,
                            DestinationCity = x.DestinationCity,
                            DestinationState = x.DestinationState,
                            PPrice1 = x.PPrice1,
                            PPrice2 = x.PPrice2,
                            PPrice3 = x.PPrice3,
                            PPrice4 = x.PPrice4,
                            PPrice5 = x.PPrice5,
                            PPrice6 = x.PPrice6,
                            PPrice7 = x.PPrice7,
                            PPrice8 = x.PPrice8,
                            PPrice9 = x.PPrice9,
                            PPrice10 = x.PPrice10,
                            TruckSize = x.Truck.Name
                        }).DynamicPageAsync(request, cancellationToken);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
