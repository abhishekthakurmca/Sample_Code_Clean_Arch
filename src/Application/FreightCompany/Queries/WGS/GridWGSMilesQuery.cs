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

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    
    public class GridWGSMilesQuery : GridQuery, IRequest<GridResult<WGSMilesDto>>
    {

    }
    public class GridWGSMilesQueryHandler : IRequestHandler<GridWGSMilesQuery, GridResult<WGSMilesDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridWGSMilesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<WGSMilesDto>> Handle(GridWGSMilesQuery request, CancellationToken cancellationToken)
        {
            try
            {               
                return await _context.Set<WGSCompanyMiles>()
                        .Where(x => x.Company_Id == request.Id && !x.IsDeleted)
                        .Include(x => x.Truck)
                        .Select(x => new WGSMilesDto
                        {
                            Id = x.Id,
                            From = x.From,
                            To = x.To,
                            LabelValue = x.LabelValue,
                            Price = x.Price,
                            TruckSize = x.Truck.Name
                        })
                        .DynamicPageAsync(request, cancellationToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

}
