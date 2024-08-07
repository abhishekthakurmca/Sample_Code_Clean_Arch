using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis.Domain.Entities
{
    public class ShipmentConfirmation
    {
        public long? RouteId { get; set; }
        public long ShipmentId { get; set; }
        public long? OrderId { get; set; }
        public string ClientReferenceNumber { get; set; }
        public int Tendered { get; set; }
        public string ShipmentStatus { get; set; }
        public string FreightType { get; set; }
        public string Site { get; set; }
        public int PlannedQuantity { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public long ClientId { get; set; }
        public string Client { get; set; }
        public string FreightCompany { get; set; }
        public long CollectionPointId { get; set; }
        public string CollectionPoint { get; set; }
        public string CollectionPointAddress { get; set; }
        public string CollectionPointAddress2 { get; set; }
        public string CollectionPointCity { get; set; }
        public string CollectionPointState { get; set; }
        public string CollectionPointZip { get; set; }
        public int ShipmentProductId { get; set; }
        public string ShipmentProduct { get; set; }
        public string ConfirmToken { get; set; }
        public string AcceptUpdateQuoteToken { get; set; }
        //public string ConfirmSecondToken { get; set; }
        //public string ConfirmThirdToken { get; set; }
        public string RejectToken { get; set; }
        public string ButtonLabel { get; set; }
        public string Email { get; set; }
        public string ShipmentContactName { get; set; }
        public string ShipmentContactEmail { get; set; }
        public string ShipmentContactPhone { get; set; }
        public string CRMName { get; set; }
        public string CRMEmail { get; set; }
        public string CRMPhone { get; set; }
        public string LogiContactName { get; set; }
        public string LogiContactEmail { get; set; }
        public string LogiContactPhone { get; set; }
        public string DockDate { get; set; }
        public string PickUpDate { get; set; }
        public string DockTime { get; set; }
        public string PickUpTime { get; set; }
        public int DockId { get; set; }
        public string ReceivingTypeId { get; set; }
        public string DroppedTrailer { get; set; }
        public string Notes { get; set; }
        public string ShipmentIds { get; set; }
        public decimal Quote { get; set; }
        public bool? IsInbound { get; set; }
        public string ExistingFreightType { get; set; }
        public string NeedHelpLink { get; set; }
        public bool IsNeedsHelp { get; set; }
        public bool IsArrangedByCustomer { get; set; }
    }
    public class InterCompanyEmail
    {
        public string Message { get; set; }
    }
}
