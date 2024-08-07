using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.LTL;
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

    public class ExportAllLTLQuery : GridQuery, IRequest<ExportFeature>
    {

    }
    public class ExportAllLTLQueryHandler : IRequestHandler<ExportAllLTLQuery, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllLTLQueryHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }


        public async Task<ExportFeature> Handle(ExportAllLTLQuery request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var data = await _context.Set<Domain.Entities.LTL_Company>()
                        .Where(x => x.Company_Id == request.Id && !x.IsDeleted)
                        .Include(x => x.Truck)
                        .Select(x => new LtlDto {
                            Id = x.Id,
                            OriginCity = x.OriginCity,
                            DestinationCity = x.DestinationCity,
                            OriginState = x.OriginState,
                            DestinationState = x.DestinationState,
                            PPrice1 = x.PPrice1,
                            PPrice2 = x.PPrice2,
                            PPrice3 = x.PPrice3,
                            PPrice4 = x.PPrice4,
                            PPrice5 = x.PPrice5,
                            PPrice6 = x.PPrice6,
                            PPrice7 = x.PPrice7,
                            PPrice8 = x.PPrice8,
                            PPrice9 = x.PPrice9,
                            PPrice10 = x.PPrice10,
                            TruckSize = x.Truck.Name
                        }).DynamicPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.Convert(data.Data),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Company-LTL-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }

}
