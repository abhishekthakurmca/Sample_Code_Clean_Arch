using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.GetFreightCompanies;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
    public class ExportAllCompanyQuery : GridQuery, IRequest<ExportFeature>
    {

    }

    public class ExportAllCompanyQueryHandler : IRequestHandler<ExportAllCompanyQuery, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllCompanyQueryHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllCompanyQuery request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.FreightCompany>()
                .ProjectTo<FreightCompanyDto>(_mapper.ConfigurationProvider)
                .DynamicExportPageAsync(request, cancellationToken);
            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Company-Profiles-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
