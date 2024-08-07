using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.ExportFiles.FreightProfiles.Company;
using Anubis.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.FTL
{
    public class GridFTLQuery : GridQuery, IRequest<GridResult<FtlDTO>>
    {

    }
    public class GridFTLQueryHandler : IRequestHandler<GridFTLQuery, GridResult<FtlDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridFTLQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<FtlDTO>> Handle(GridFTLQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Set<FTL_Company>()
                         .Where(x => x.Company_Id == request.Id && !x.IsDeleted)
                         .Include(x => x.Truck)
                         .Select(x => new FtlDTO
                         {
                             Id = x.Id,
                             OriginCity = x.OriginCity,
                             OriginState = x.OriginState,
                             DestinationCity = x.DestinationCity,
                             DestinationState = x.DestinationState,
                             Price = x.Price,
                             TruckSize = x.Truck.Name
                        })
                        .DynamicPageAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
