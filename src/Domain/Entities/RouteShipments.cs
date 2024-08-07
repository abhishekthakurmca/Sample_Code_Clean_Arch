using Anubis.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class RouteShipments : AuditableEntity
    {
        public RouteShipments()
        {
            ShipmentAknowledges = new List<ShipmentAknowledge>();
            ShipmentStatusList = new List<ShipmentStatus>();
        }
        public long Id { get; set; }
        public long? Route_Id { get; set; }
        public long ShipmentId { get; set; }
        public long? OrderId { get; set; }
        public string ClientReferenceNumber { get; set; }
        public int Tendered { get; set; }
        public string ShipmentStatus { get; set; }
        public string FreightType { get; set; }
        public int PlannedQty { get; set; }
        public decimal? Weight { get; set; }
        public int StatusId { get; set; }
        public long ClientId { get; set; }
        public string Client { get; set; }
        public long CollectionPointId { get; set; }
        public string CollectionPoint { get; set; }
        public string CollectionPointAddress { get; set; }
        public string CollectionPointAddress2 { get; set; }
        public string CollectionPointCity { get; set; }
        public string CollectionPointState { get; set; }
        public string CollectionPointZip { get; set; }
        public long SiteId { get; set; }
        public string Site { get; set; }
        public string SiteName { get; set; }
        public int ShipmentProductId { get; set; }
        public string ShipmentProduct { get; set; }

        [StringLength(250)]
        public string Quote { get; set; }
        public long InternalReferenceID { get; set; }
        public string InternalNotes { get; set; }
        public int? TransportDays { get; set; }
        public bool IsAuto { get; set; }
        public bool IsInbound { get; set; }
        public bool IsArrangedByCustomer { get; set; }
        public DateTime? TenderPendingDate { get; set; }
        public DateTime? TenderPendingApprovalDate { get; set; }
        public DateTime? RejectShipmentDate { get; set; }
        public DateTime? QuoteApprovalDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string AccountType { get; set; }
        public bool IsTransfer { get; set; }
        public bool CanTransfer { get; set; }
        public int DockID { get; set; }
        public string DockName { get; set; }
        public int? RecevingTypeId { get; set; }
        public string ReceivingTypeName { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string CRMName { get; set; }
        public string CRMEmail { get; set; }
        public string CRMPhone { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? DockDate { get; set; }
        public string DockTime { get; set; }
        public DateTime? OrderDate { get; set; }
        public string PickUpTime { get; set; }
        public bool IsInterCompany { get; set; }
        public string InterCompanyShipmentId { get; set; }
        public bool IsVerifyDelivery { get; set; }
        public string RejectReason { get; set; }
        public bool IsNeedsHelp { get; set; }
        public string NeedsHelpNote { get; set; }
        public string Notes { get; set; }
        public DateTime? TransferredDate { get; set; }
        public DateTime? RouteDate { get; set; }
        [ForeignKey("RouteShipment_Id")]
        public IList<ShipmentAknowledge> ShipmentAknowledges { get; set; }
        [ForeignKey("RouteShipment_Id")]
        public IList<ShipmentStatus> ShipmentStatusList { get; set; }
        //public virtual Route _Route { get; set; }
        public int AvgShipWeight { get; set; }
        public decimal FreightCostResponsibilityPercentage { get; set; }
    }
}
