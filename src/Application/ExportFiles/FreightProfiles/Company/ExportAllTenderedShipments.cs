using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Mappings;
using Anubis.Application.Common.Models;
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
    public class ExporTenderedShipmentDTO : IMapFrom<RouteShipments>
    {
        public long ShipmentId { get; set; }
        public long? Route_Id { get; set; }
        public string ClientName { get; set; }
        public int AgingDays { get; set; }
        public DateTime? OrderCreateDate { get; set; }
        public DateTime? TenderDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DockDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ERISite { get; set; }
        public bool InOut { get; set; }
        public bool CustomerArranged { get; set; }
        public string ReceivingType { get; set; }
        public string FormType { get; set; }
        public int PlannedQty { get; set; }
        public string FreightCompany { get; set; }
        public string TeamMemberName { get; set; }

    }
    public class ExportAllTenderedShipments : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllTenderedShipmentsHandler : IRequestHandler<ExportAllTenderedShipments, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllTenderedShipmentsHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllTenderedShipments request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var (agingDays, receivingFormType, routeShipmentId, stateCity, inBoundArrangedByCustomer) = request.Filter.Any() ? request.CustomFilterValue() :
                     (default(DateTime), string.Empty, 0, string.Empty, string.Empty);
            var result = await (from rs in _context.Set<RouteShipments>()
                                join r in _context.Set<Route>().Include(x => x._Company)
                                    on rs.Route_Id equals r.Id
                                join t in _context.Set<TeamMemberShipments>().Include(x => x.LU_TeamMember)
                                on rs.ShipmentId equals t.ShipmentId
                                into teamGroup
                                from team in teamGroup.DefaultIfEmpty()
                                where rs.Tendered == (int)Tender.SendEmail && (request.IsNeedHelp ? rs.IsNeedsHelp : true) &&
                                 (
                                    (string.IsNullOrEmpty(receivingFormType) || rs.FreightType.ToLower().Contains(receivingFormType.ToLower()) || rs.ReceivingTypeName.ToLower().Contains(receivingFormType.ToLower())) &&
                                    (string.IsNullOrEmpty(stateCity) || rs.CollectionPointCity.ToLower().Contains(stateCity) || rs.CollectionPointState.Contains(stateCity)) &&
                                    (routeShipmentId <= 0 || rs.Route_Id == routeShipmentId || rs.ShipmentId == routeShipmentId) &&
                                    (
                                        (string.IsNullOrEmpty(inBoundArrangedByCustomer) && rs.Route_Id != null) ||
                                        (inBoundArrangedByCustomer == "in" && rs.IsInbound == true) ||
                                        (inBoundArrangedByCustomer == "out" && rs.IsInbound == false) ||
                                        (inBoundArrangedByCustomer == "yes" && rs.IsArrangedByCustomer == true) ||
                                        (inBoundArrangedByCustomer == "no" && rs.IsArrangedByCustomer == false)
                                    ) &&
                                    (
                                        agingDays == default(DateTime) ||
                                        (rs.TenderPendingDate != null && rs.TenderPendingDate.Value.Day == agingDays.Day && rs.TenderPendingDate.Value.Month == agingDays.Month && rs.TenderPendingDate.Value.Year == agingDays.Year)
                                    )
                                )
                                select new ExporTenderedShipmentDTO
                                {
                                    ShipmentId = rs.ShipmentId,
                                    Route_Id = rs.Route_Id,
                                    ClientName = rs.Client,
                                    AgingDays = (int)(DateTime.Now.Date - (rs.TenderPendingDate ?? DateTime.Now).Date).TotalDays,
                                    OrderCreateDate = rs.OrderDate,
                                    TenderDate = rs.TenderPendingDate,
                                    PickUpDate = r.PickUpDate,
                                    DockDate = r.DockDate,
                                    City = rs.CollectionPointCity,
                                    State = rs.CollectionPointState,
                                    ERISite = rs.Site,
                                    InOut = rs.IsInbound,
                                    CustomerArranged = rs.IsArrangedByCustomer,
                                    ReceivingType = rs.ReceivingTypeName,
                                    FormType = rs.FreightType,
                                    PlannedQty = rs.PlannedQty,
                                    FreightCompany = r._Company == null ? "" : r._Company.Name,
                                    TeamMemberName = team != null ? team.LU_TeamMember.Name : string.Empty
                                }
                                )
                                .DynamicPageAsync(request, cancellationToken);

            return new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(result.Data),
                ContentType = "text/csv",
                FileName = $"Tender-Pending-Shipments-{DateTime.Now.Ticks}.csv"
            };
        }
    }
}
