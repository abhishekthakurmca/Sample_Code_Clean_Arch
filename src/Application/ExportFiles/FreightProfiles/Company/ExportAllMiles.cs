using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Mappings;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.Dashboard;
using Anubis.Application.FreightCompany.Queries.LinkedClients;
using Anubis.Domain.Entities;
using Anubis.Domain.Enums;
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
    public class WGSMiles
    {
        public string Miles { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public decimal MinimumCharge { get; set; }
        public string TruckSize { get; set; }
    }

    public class ExportAllMiles : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllMilesHandler : IRequestHandler<ExportAllMiles, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;
        public ExportAllMilesHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }
        public async Task<ExportFeature> Handle(ExportAllMiles request, CancellationToken cancellationToken)
        {           
                request.Page = 1;  
                var data = await _context.Set<Domain.Entities.WGSCompanyMiles>()
                        .Where(wg => wg.Company_Id == request.Id && !wg.IsDeleted)
                        .Include(wg => wg.Truck)
                        .Select(wg => new WGSMiles {
                            Miles = wg.LabelValue,
                            From = wg.From,
                            To = wg.To,
                            MinimumCharge = wg.Price,
                            TruckSize = wg.Truck.Name
                        }).DynamicExportPageAsync(request, cancellationToken);

                return new ExportFeature
                {
                    Content = _excelConverter.Convert(data.Data),
                    ContentType = "application/vnd.ms-excel",
                    FileName = $"WGS-Miles-{DateTime.Now.Ticks}.xlsx"
                };            
        }
    }
}
