using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Anubis.Domain.Enums
{  
    public enum Tender
    {
        SendEmail = 0,
        Confirmed = 1,
        Reject = 2,
        ApprovalPending = 3,
        Cancel = 4,
        QuoteApprovalPending = 5,
        CapacityNotAvailable = 6,
    }
    public enum FreightType
    {
        [Description("FTL")]
        FTL = 1,
        [Description("LTL")]
        LTL = 2,
        [Description("WGS/Decommission")]
        WGS = 7,

        [Description("Arranged by Customer")]
        ArrangedbyCustomer = 17,

        [Description("Onsite Destruction/Erasure")]
        OnsiteDestructionErasure = 18
    }

    public enum TitanShipmentStatus
    {
        ReadyForScheduling = 1,
        Confirmed = 2,
        Reviewed= 6
    }
    public enum DockSchedulerStatus
    {
        Active = 1,
        Confirmed = 2,
    }
    public static class Status
    {
        public static Dictionary<int, string> statusDictionary = new Dictionary<int, string>
        {
            {1, "Ready For Scheduling"},
            {2, "Ready For Shipment"},
            {3, "Shipment Confirmed"},
            {4, "Shipment Rejected"},
            {5, "Quote Approval Pending"},
            {6, "In-Route"},
            {7, "Backlog"},
            {8, "Delivered"},
            {9, "Capacity Not Available"},
            {10, "Shipment Approval Pending" }
        };
        public static Dictionary<int, string> statusTitanDictionary = new Dictionary<int, string>
        {
            {0, "Pending"},
            {1, "Ready For Scheduling"},
            {2, "Scheduled"},
            {3, "Unloaded" },
            {4, "Loaded"},
            {5, "Canceled"},
            {6, "Reviewed"},
            {7, "WIP"},
            {8, "Review Pending"},
            {9, "Labels Generated"},
            {10, "Delivered" },
            {14, "Awaiting Pricing" },
            {15, "Process Pending" }
        };

        public static string GetStatus(int key)
        {
            string result;
            if (statusDictionary.TryGetValue(key, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public static string GetTitanStatus(int key)
        {
            string result;
            if (statusTitanDictionary.TryGetValue(key, out result))
            {
                return result;
            }
            return null;
        }

        public enum ShipmentTypeStatus
        {
            Pending = 0,
            ReadyForScheduling = 1,
            Scheduled = 2,
            Unloaded = 3,
            Loaded = 4,
            Canceled = 5,
            Reviewed = 6,
            WIP = 7,
            ReviewPending = 8,
            LabelsGenerated = 9,
            Delivered = 10,
            LabelsPrinted = 11,
            AwaitingPricing = 14,
            ProcessPending = 15
        }
    }

    public static class FreightTypes
    {
        public static int? GetFreightTypeId(string freightType)
        {
            foreach (var enumValue in Enum.GetValues(typeof(FreightType)))
            {
                if (enumValue is FreightType freightTypeEnum && GetDescription(freightTypeEnum).Equals(freightType, StringComparison.OrdinalIgnoreCase))
                {
                    return (int)enumValue;
                }
            }

            return null;
        }
        
        public static string GetDescription(this FreightType key)
        {
            var descriptionAttribute = typeof(FreightType)
                .GetField(key.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault();

            return descriptionAttribute?.Description ?? key.ToString();
        }

        public static string GetFreightTypeDescription(int? freightTypeID)
        {
            return freightTypeID.HasValue ? ((FreightType)freightTypeID).GetDescription() ?? null : null;
        }      

    }
    public static class ExcludedSites
    {
        public static string[] Sites = { "badin, nc", "dallas, tx", "denver, co", "fresno, ca", "goodyear, az", "holliston, ma", "lincoln park, nj", "plainfield, in", "seattle, wa" };
    }

    public static class ExcludedInternationalSites
    {
        public static string[] Sites = { "eri satellite site", "eri brokerage" , "eri international site" };
    }

    public static class ShipmentStatusConstants
    {
        public static string Transferred = "Transferred";
        public static string Tendered = "Tendered";
        public static string Reject = "Rejected";
        public static string QuoteApproval = "Quote Approval";
        public static string Confirmed = "Confirmed";
    }
}
