using Anubis.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.Dropdowns
{
   public class FreightCodeListQuery : IRequest<BasicInformationCompanyDto>
    {
        public long Company_Id { get; set; }
    }
    public class FreightCodeListQueryHandler : IRequestHandler<FreightCodeListQuery, BasicInformationCompanyDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FreightCodeListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BasicInformationCompanyDto> Handle(FreightCodeListQuery request, CancellationToken cancellationToken)
        {
            var result = new BasicInformationCompanyDto();
                result.FreightCompanyCodes = await _context.Set<Domain.Entities.FreightCompanyCodes>().Where(x => x.Company_Id == request.Company_Id).ToListAsync(cancellationToken);
            return result;

        }
    }
}
