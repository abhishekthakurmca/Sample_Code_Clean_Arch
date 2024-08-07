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

    public class ExportAllFTLQuery : GridQuery, IRequest<ExportFeature>
    {

    }

    public class ExportAllFTLQueryHandler : IRequestHandler<ExportAllFTLQuery, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllFTLQueryHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllFTLQuery request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.FTL_Company>()
                    .Where(x => x.Company_Id == request.Id && !x.IsDeleted)
                    .Include(x => x.Truck)
                    .Select(x => new FtlDTO
                    {
                        Id = x.Id,
                        OriginCity = x.OriginCity,
                        DestinationCity = x.DestinationCity,
                        OriginState = x.OriginState,
                        DestinationState = x.DestinationState,
                        Price = x.Price,
                        TruckSize = x.Truck.Name
                    }).DynamicExportPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Company-FTL-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
