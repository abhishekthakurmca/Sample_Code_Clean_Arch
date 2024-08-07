using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.FreightCode;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
    public class ExportAllFreightCode : GridQuery, IRequest<ExportFeature>
    {
    }
    public class EportAllFreightCodeHandler : IRequestHandler<ExportAllFreightCode, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public EportAllFreightCodeHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }


        public async Task<ExportFeature> Handle(ExportAllFreightCode request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.FreightCompanyCodes>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                .ProjectTo<FreightCodeDto>(_mapper.ConfigurationProvider)
                    .DynamicPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Company-FreightCode-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
