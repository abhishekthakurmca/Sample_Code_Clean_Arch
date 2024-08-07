using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
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

namespace Anubis.Application.ExportFiles.FreightProfiles.Company
{
    public class ExportAllWGSWeight: GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllWGSWeightQueryHandler : IRequestHandler<ExportAllWGSWeight, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllWGSWeightQueryHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllWGSWeight request, CancellationToken cancellationToken)
        {

            var data = await _context.Set<Domain.Entities.FTL_Company>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true).Select(
                x => new
                {
                    OriginCity = x.OriginCity ?? "",
                    DestinationCity = x.DestinationCity ?? "",
                    Price = x.Price.ToString()
                })

            .DynamicPageAsync(request, cancellationToken);
            var datas = await _context.Set<Domain.Entities.FTL_Company>().Where(x => x.Company_Id == request.Id && x.IsDeleted != true)
                .ProjectTo<FtlDTO>(_mapper.ConfigurationProvider)
                .DynamicExportPageAsync(request, cancellationToken);
            return new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(data.Data),
                ContentType = "text/csv",
                FileName = $"Company-Profiles-{DateTime.Now.Ticks}.csv"
            };
        }
    }

}
