using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.HistoricFreight;
using Anubis.Domain.Entities;
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
    public class ExportAllHistoricFreightRates : GridQuery, IRequest<ExportFeature>
    {
    }
    public class ExportAllHistoricFreightRatesHandler :  IRequestHandler<ExportAllHistoricFreightRates, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllHistoricFreightRatesHandler(IApplicationDbContext context, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }
        public async Task<ExportFeature> Handle(ExportAllHistoricFreightRates request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var freightRatesList = new List<FreightRates>();
            var freightInvoice = await _context.Set<FreightInvoices>()
                .Include(inv => inv._Route).ThenInclude(r => r.RouteShipments)
                .OrderByDescending(i => i.GrandTotal)
                .Where(x => x._Route.Company_Id == request.Id && x.IsDeleted != true)
               .DynamicPageAsync(request, cancellationToken);


            foreach (var x in freightInvoice.Data.ToList())
            {
                if (x._Route == null || x._Route.RouteShipments.Count == 0)
                {
                    continue;
                }
                freightRatesList.Add(
                new FreightRates
                {
                    RouteId = x.Route_Id,
                    CollectionPoint = x._Route.RouteShipments.FirstOrDefault().CollectionPoint,
                    Site = x._Route.RouteShipments.FirstOrDefault().Site,
                    CollectionLocation = x._Route.RouteShipments.FirstOrDefault().CollectionPointAddress,
                    ScheduledDate = x._Route.DockDate.Value.ToString("yyyy-MM-dd"),
                    PickUpDate = x._Route.PickUpDate.Value.ToString("yyyy-MM-dd"),
                    Quote = x.Quote,
                    ShipmentType = x._Route.RouteShipments.FirstOrDefault().FreightType,
                    FreightCharges = x.GrandTotal
                }
                );
            }

            return new ExportFeature
            {
                Content = _excelConverter.Convert(freightRatesList),
                ContentType = "application/vnd.ms-excel",
                FileName = $"Historic-Freight-Rates-{DateTime.Now.Ticks}.xlsx"
            };
        }
    }
}
