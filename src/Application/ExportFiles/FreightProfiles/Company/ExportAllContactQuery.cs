using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.GetCompanyContacts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{

    public class ExportAllContactQuery : GridQuery, IRequest<ExportFeature>
    {

    }

    public class ExportAllContactQueryHandler : IRequestHandler<ExportAllContactQuery, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllContactQueryHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllContactQuery request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.CompanyContact>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
    .ProjectTo<CompanyContactDto>(_mapper.ConfigurationProvider)
    .DynamicExportPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Company-Contact-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
