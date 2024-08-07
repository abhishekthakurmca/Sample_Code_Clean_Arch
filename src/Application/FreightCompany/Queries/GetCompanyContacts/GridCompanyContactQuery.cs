

using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.GetCompanyContacts
{

    public class GridCompanyContactQuery : GridQuery, IRequest<GridResult<CompanyContactDto>>
    {

    }
    public class GridCompanyContactQueryHandler : IRequestHandler<GridCompanyContactQuery, GridResult<CompanyContactDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GridCompanyContactQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GridResult<CompanyContactDto>> Handle(GridCompanyContactQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.Entities.CompanyContact>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
    .ProjectTo<CompanyContactDto>(_mapper.ConfigurationProvider)
    .DynamicPageAsync(request, cancellationToken);
        }
    }


}
