using Anubis.Application.Common.Helper;
using Anubis.Application.Common.Interfaces;
using Anubis.Application.Common.Mappings;
using Anubis.Application.Common.Models;
using Anubis.Application.FreightCompany.Queries.Dashboard;
using Anubis.Application.FreightCompany.Queries.GetFreightCompanies;
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
    public class ExporRejectedtShipmentDTO : IMapFrom<RouteShipments>
    {
        public long ShipmentId { get; set; }
        public string ClientName { get; set; }
        public int AgingDays { get; set; }
        public DateTime? OrderCreateDate { get; set; }
        public string ShipmentProduct { get; set; }
        public DateTime? RejectShipmentDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ShipmentStatus { get; set; }
        public bool InOut { get; set; }
        public bool CustomerArranged { get; set; }
        public string ReceivingType { get; set; }
        public int PlannedQty { get; set; }
        public string FormType { get; set; }  
        
        public string TeamMemberName { get; set; }   
        
    }
    public class ExportAllRejectedShipments : GridQuery, IRequest<ExportFeature>
    {
    }

    public class ExportAllRejectedShipmentsHandler : IRequestHandler<ExportAllRejectedShipments, ExportFeature>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IExcelConverter _excelConverter;
        private readonly IMapper _mapper;

        public ExportAllRejectedShipmentsHandler(IApplicationDbContext context, IFileService fileService, IExcelConverter excelConverter, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _excelConverter = excelConverter;
            _mapper = mapper;
        }

        public async Task<ExportFeature> Handle(ExportAllRejectedShipments request, CancellationToken cancellationToken)
        {
            request.Page = 1;
            var (agingDays, receivingFormType, routeShipmentId, stateCity, inBoundArrangedByCustomer) = request.Filter.Any() ? request.CustomFilterValue() :
                     (default(DateTime), string.Empty, 0, string.Empty, string.Empty);
            var result = await (from rs in _context.Set<RouteShipments>()
                                join t in _context.Set<TeamMemberShipments>().Include(x => x.LU_TeamMember)
                                    on rs.ShipmentId equals t.ShipmentId
                                    into teamGroup
                                from team in teamGroup.DefaultIfEmpty()
                                where (rs.Tendered == (int)Tender.Reject || rs.Tendered == (int)Tender.CapacityNotAvailable)
                                && (
            (string.IsNullOrEmpty(receivingFormType) || rs.FreightType.ToLower().Contains(receivingFormType) || rs.ReceivingTypeName.ToLower().Contains(receivingFormType)) &&
            (string.IsNullOrEmpty(stateCity) || rs.CollectionPointCity.ToLower().Contains(stateCity) || rs.CollectionPointState.Contains(stateCity)) &&
            (
                (string.IsNullOrEmpty(inBoundArrangedByCustomer)) ||
                (inBoundArrangedByCustomer == "in" && rs.IsInbound == true) ||
                (inBoundArrangedByCustomer == "out" && rs.IsInbound == false) ||
                (inBoundArrangedByCustomer == "yes" && rs.IsArrangedByCustomer == true) ||
                (inBoundArrangedByCustomer == "no" && rs.IsArrangedByCustomer == false)
            ) &&
            (agingDays == default(DateTime) || (rs.RejectShipmentDate != null && rs.RejectShipmentDate.Value.Day == agingDays.Day && rs.RejectShipmentDate.Value.Month == agingDays.Month && rs.RejectShipmentDate.Value.Year == agingDays.Year)))
                                select new ExporRejectedtShipmentDTO
                                {
                                    ShipmentId = rs.ShipmentId,
                                    ClientName = rs.Client,
                                    AgingDays = rs.RejectShipmentDate != null ? (int)(DateTime.Now.Date - rs.RejectShipmentDate.Value.Date).TotalDays : 0,
                                    OrderCreateDate = rs.OrderDate,
                                    ShipmentProduct = rs.ShipmentProduct,
                                    RejectShipmentDate = rs.RejectShipmentDate,
                                    City = rs.CollectionPointCity,
                                    State = rs.CollectionPointState,
                                    ShipmentStatus = rs.ShipmentStatus,
                                    InOut = rs.IsInbound,
                                    CustomerArranged = rs.IsArrangedByCustomer,
                                    ReceivingType = rs.ReceivingTypeName,
                                    FormType = rs.FreightType,
                                    PlannedQty = rs.PlannedQty,
                                    TeamMemberName = team != null ? team.LU_TeamMember.Name : string.Empty,
                                }
              )
              .DynamicPageAsync(request, cancellationToken);
            return  new ExportFeature
            {
                Content = _excelConverter.ConvertCsv(result.Data),
                ContentType = "text/csv",
                FileName = $"Rejected-Shipments-{DateTime.Now.Ticks}.csv"
            }; 
        }
    }
}
