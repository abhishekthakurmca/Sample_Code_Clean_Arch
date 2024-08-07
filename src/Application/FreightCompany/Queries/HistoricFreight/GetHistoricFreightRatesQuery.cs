using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Models;
using Anubis.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.FreightCompany.Queries.HistoricFreight
{
    public class FreightRates
    {
        public long RouteId { get; set; }
        public string CollectionPoint { get; set; }
        public string Site { get; set; }
        public string CollectionLocation { get; set; }
        public string CollectionLocationToolTip { get; set; }
        public string PickUpDate { get; set; }
        public string ScheduledDate { get; set; }
        public decimal FreightCharges { get; set; }
        public decimal Quote { get; set; }
        public string ShipmentType { get; set; }
    }
    public class GetHistoricFreightRatesQuery : GridQuery, IRequest<GridResult<FreightRates>>
    {
    }
    public class GetHistoricFreightRatesQueryHandler : IRequestHandler<GetHistoricFreightRatesQuery, GridResult<FreightRates>>
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricFreightRatesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GridResult<FreightRates>> Handle(GetHistoricFreightRatesQuery request, CancellationToken cancellationToken)
        {
            var result = new GridResult<FreightRates>();
            var freightRatesList = new List<FreightRates>();
            var freightInvoice = await _context.Set<FreightInvoices>()
                .Include(inv => inv._Route).ThenInclude(r => r.RouteShipments).OrderByDescending(x=>x.GrandTotal)
                .Where(x => x._Route.Company_Id == request.Id && x.IsDeleted != true)
               .DynamicPageAsync(request, cancellationToken);

            if (freightInvoice.Data.Count == 0)
                return result;

            foreach (var x in freightInvoice.Data.ToList())
            {
                if(x._Route == null || x._Route.RouteShipments.Count == 0)
                {
                    continue;
                }
                freightRatesList.Add(
                new FreightRates
                {
                    RouteId = x.Route_Id,
                    CollectionPoint = x._Route.RouteShipments.FirstOrDefault().CollectionPoint,
                    Site = x._Route.RouteShipments.FirstOrDefault().Site,
                    Quote = x.Quote,
                    CollectionLocation = x._Route.RouteShipments.FirstOrDefault().CollectionPointAddress,
                    CollectionLocationToolTip = $"{ x._Route.RouteShipments.FirstOrDefault().CollectionPointAddress}, { x._Route.RouteShipments.FirstOrDefault().CollectionPointCity }, { x._Route.RouteShipments.FirstOrDefault().CollectionPointState }",
                    ScheduledDate = x._Route.DockDate.Value.ToString("yyyy-MM-dd"),
                    PickUpDate = x._Route.PickUpDate.Value.ToString("yyyy-MM-dd"),
                    ShipmentType = x._Route.RouteShipments.FirstOrDefault().FreightType,
                    FreightCharges = x.GrandTotal
                }
                );
            }

            result.Data = freightRatesList;
            return result;
        }
    }
}
