using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
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

namespace Anubis.Application.FreightCompany.Queries.WGS
{
    public class WGSAccesrailsDto
    {
        public long Id { get; set; }
        public long Company_Id { get; set; }

        public long FreightCode_Id { get; set; }

        public bool IsFixedPrice { get; set; }
        public decimal DefaultPrice { get; set; }
        public string FreightCode { get; set; }

    }
    public class GridWGSAccesrailsQuery : GridQuery, IRequest<GridResult<WGSAccesrailsDto>>
    {
    }
    public class GridWGSAccesrailsQueryHandler : IRequestHandler<GridWGSAccesrailsQuery, GridResult<WGSAccesrailsDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridWGSAccesrailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<WGSAccesrailsDto>> Handle(GridWGSAccesrailsQuery request, CancellationToken cancellationToken)
        {
    //        var aa= await _context.Set<WGSAccesrails>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
    // .Include(x => x.FreightCompanyCodes)
    //.DynamicPageAsync(request, cancellationToken);

           var pp= await _context.Set<WGSAccesrails>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
     .Include(x => x.FreightCompanyCodes).Select(x => new WGSAccesrailsDto
     {
         Company_Id = x.Company_Id,
         DefaultPrice = x.FreightCompanyCodes.DefaultPrice,
         FreightCode = x.FreightCompanyCodes.Name,
         FreightCode_Id = x.FreightCode_Id,
         IsFixedPrice = x.IsFixedPrice,
         Id = x.Id
     })
    .DynamicPageAsync(request, cancellationToken);

            return pp;
    //        return await _context.Set<WGSAccesrails>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
    // .Include(x => x.FreightCompanyCodes).Select(x => new WGSAccesrailsDto
    // {
    //     Company_Id = x.Company_Id,
    //     DefaultPrice = x.DefaultPrice,
    //     FreightCode = x.FreightCompanyCodes.Name,
    //     FreightCode_Id = x.FreightCode_Id,
    //     IsFixedPrice = x.IsFixedPrice,
    //     Id = x.Id
    // })
    //.DynamicPageAsync(request, cancellationToken);
        }
    }
}
