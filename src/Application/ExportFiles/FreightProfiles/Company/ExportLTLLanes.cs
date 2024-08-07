using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.LTL;
using AutoMapper;
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
   public class ExportLTLLanes : GridQuery, IRequest<ExportFeature>
    {
    }
    public class ExportLTLLanesHandler : IRequestHandler<ExportLTLLanes, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportLTLLanesHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportLTLLanes request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.LTL_Company>()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Truck)
                .Select(ltl => new LtlDto {
                    Id = ltl.Id,
                    OriginCity = ltl.OriginCity,
                    OriginState = ltl.OriginState,
                    DestinationCity = ltl.DestinationCity,
                    DestinationState = ltl.DestinationState,
                    PPrice1 = ltl.PPrice1,
                    PPrice2 = ltl.PPrice2,
                    PPrice3 = ltl.PPrice3,
                    PPrice4 = ltl.PPrice4,
                    PPrice5 = ltl.PPrice5,
                    PPrice6 = ltl.PPrice6,
                    PPrice7 = ltl.PPrice7,
                    PPrice8 = ltl.PPrice8,
                    PPrice9 = ltl.PPrice9,
                    PPrice10 = ltl.PPrice10,
                    TruckSize = ltl.Truck.Name
                }).DynamicExportPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"LTL-All-Lanes-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
